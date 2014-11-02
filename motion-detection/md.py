#!C:\Python27\python.exe

import sys
sys.path.append("..")
import os
import time, datetime
import xml.etree.ElementTree as ET
from SimpleCV import JpegStreamCamera, VideoStream
from subprocess import Popen
from os.path import dirname, join

from server import alerts, config

class Md:

    def __init__(self):
        conf = config.Config()
        self.lastmod = conf.get_last_mod_time()

    def get_cameras_data(self):

        """ Get camera data from config file """

        conf = config.Config()

        return conf.get_cameras_for_md()

    def get_cameras_stream(self, cameras_data):

        """ Get cameras as SimplCV jpeg stream object """

        cameras = []

        for cam in cameras_data:
            path = "%s:%s/%s" % (cam['url'], cam['port'], cam['stream'])
            cameras.append(JpegStreamCamera(path))

        return cameras

    def start(self):

        """ Start detecting motion """

        cameras_data = self.get_cameras_data()
        cameras = self.get_cameras_stream(cameras_data)

        conf = config.Config()
        md = conf.get_md()

        # continue checking until md is off by the user
        while md['on'] == 'true':

            # check if config file has changed and update data if needed
            modtime = conf.get_last_mod_time()
            if(modtime > self.lastmod):
                cameras_data = self.get_cameras_data()
                cameras = self.get_cameras_stream(cameras_data)
                
                conf = config.Config()
                md = conf.get_md()
                self.lastmod = modtime

            # loop through all the cameras with motion detection enabled
            for cam, cam_data in zip(cameras, cameras_data):

                # get a frame from the current camera
                frame1 = cam.getImage()

                # if motion was not detected
                if not cam_data['md_found']:

                    # get a second frame for comparison
                    frame2 = cam.getImage()
                    diff = frame2 - frame1

                    # get measure of change in the frames
                    matrix = diff.getNumpy()
                    mean = matrix.mean()

                    # check if the amount of change exceeds the threshold                                        
                    if mean >= cam_data['threshold']:

                        # create a new object for recording
                        now = time.strftime("%d-%m-%Y_%H-%M-%S", time.gmtime())
                        file_name = "cam-%s-%s.avi" % (cam_data['name'].replace(" ", ""), now)
                        file_path = "server/recordings/"
                        cam_data['vs'] = VideoStream(file_path + file_name, 25, True)

                        # save to alerts.xml and send email if requested
                        alert_id = alerts.save_alert(cam_data['name'], now, file_name)
                        if(cam_data['alerts'] == 'true'):
                            cmd = "python md/sendmail.py %s" % cam_data['name']
                            Popen(cmd)                        

                        cam_data['md_found'] = True
                        cam_data['record_time_start'] = time.time()

                # motion already detected. just record.
                if cam_data['md_found']:
                    # save current frame to video stream
                    cam_data['vs'].writeFrame(frame1)

                    # tick the timer
                    timediff = int(time.time() - cam_data['record_time_start'])
                    if timediff >= cam_data['record_time']:

                        # start avi to mp4 conversion (popen it)
                        old_file_name = cam_data['vs'].filename
                        new_file_name = "%s.mp4" % old_file_name[:-4]

                        cmd = "python md/convertvideo.py %s %s %s" % (alert_id, old_file_name, new_file_name)
                        Popen(cmd)

                        # reset motion detection values
                        cam_data['md_found'] = False
                        cam_data['record_time_start'] = None
                        cam_data['vs'] = None

                time.sleep(0.2)


if __name__ == "__main__":
    md = Md()
    md.start()