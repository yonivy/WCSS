#!C:\Python27\python.exe

import xml.etree.ElementTree as ET
from xml.dom import minidom
import sys, os
from os.path import dirname, join
import time, datetime

CONF_FILE = join(dirname(__file__), 'data', 'config.xml')

class Config:
	def __init__(self):
		self.file = ET.parse(CONF_FILE)
		self.root = self.file.getroot()

	def get_last_mod_time(self):

		""" Get last time config file was changed """

		return os.path.getmtime(CONF_FILE)	

	def get_user(self):

		""" Get user data from configuration file """

		user = self.root.find('user')

		out = {}

		out['name'] = user.findtext('name')
		out['email'] = user.findtext('email')
		out['password'] = user.findtext('password')

		return out	

	def set_user(self, data):

		""" Set user data in configuration file """

		user = self.root.find('user')

		user.find('name').text = str(data['name'])
		user.find('email').text = str(data['email'])

		self.file.write(CONF_FILE)

	def get_lserver(self):

		""" Get local server data from configuration file """

		lserver = self.root.find('localServer')

		out = {}

		out['url'] = lserver.findtext('url')
		out['port'] = lserver.findtext('port')

		return out

	def set_lserver(self, data):

		""" Set local server data in configuration file """

		lserver = self.root.find('localServer')

		lserver.find('url').text = data['url']
		lserver.find('port').text = data['port']

		self.file.write(CONF_FILE)

	def get_web_app_server(self):

		""" Get web app server data from configuration file """

		web_app = self.root.find('webAppServer')

		out = {}

		out['url'] = web_app.findtext('url')
		out['port'] = web_app.findtext('port')

		return out

	def set_web_app_server(self, data):

		""" Set web app server data in configuration file """

		web_app = self.root.find('webAppServer')

		web_app.find('url').text = data['url']
		web_app.find('port').text = data['port']

		self.file.write(CONF_FILE)

	def get_md(self):

		""" Get motion detection data from configuration file """

		md = self.root.find('motionDetection')

		out = {}

		out['on'] = md.get('on')
		out['alerts'] = md.findtext('alerts')

		return out

	def set_md(self, data):

		""" Set motion detection data in configuration file """

		md = self.root.find('motionDetection')

		if 'md-on' in data:
			md.set('on', 'true')
		else:
			md.set('on', 'false')

		if 'alerts-on' in data:
			md.find('alerts').text = 'true'
		else:
			md.find('alerts').text = 'false'

		self.file.write(CONF_FILE)

	def get_cameras(self):

		""" Get all cameras data from configuration file """

		cameras = self.root.find('cameras')

		cams = []

		for camera in cameras.findall('camera'):
			cam = {}

			cam['id'] = camera.get('id')
			cam['name'] = camera.findtext('name')
			cam['url'] = camera.findtext('url')
			cam['port'] = camera.findtext('port')
			cam['stream'] = camera.findtext('streamPath')
			cam['md'] = camera.findtext('motionDetection')			
			cam['threshold'] = camera.findtext('threshold')
			cam['alerts'] = camera.findtext('alerts')
			cam['rec_time'] = camera.findtext('recordTime')
			

			cams.append(cam)
		
		return cams

	def get_cameras_for_md(self):

		""" Get all cameras data for the md module """

		cameras = self.root.find('cameras')

		cams = []

		for camera in cameras.findall('camera'):
			if camera.findtext('motionDetection') == 'true':
				cam = {}

				cam['id'] = camera.get('id')
				cam['name'] = camera.findtext('name')
				cam['url'] = camera.findtext('url')
				cam['port'] = camera.findtext('port')
				cam['stream'] = camera.findtext('streamPath')
				cam['md'] = camera.findtext('motionDetection')			
				cam['threshold'] = int(camera.findtext('threshold'))
				cam['alerts'] = camera.findtext('alerts')

				x = time.strptime(camera.findtext('recordTime').split(',')[0],'%M:%S')
				cam['record_time'] = int(datetime.timedelta(hours=0,minutes=x.tm_min,seconds=x.tm_sec).total_seconds())
				
				cam['md_found'] = False
				cam['record_time_start'] = None
				cam['vs'] = None

				cams.append(cam)
		
		return cams

	def set_camera(self, data):

		""" Set camera data in configuration file """		

		cameras = self.root.find('cameras')
		camera = cameras.find("./camera[@id='" + data['id'] + "']")

		prefix = "cam-%s-" % data['id']

		camera.find('threshold').text = str(data[prefix + 'threshold'])
		
		if prefix + 'md-on' in data:
			if data[prefix + 'md-on'] in ('on', 'true'):
				camera.find('motionDetection').text = 'true'
			else:
				camera.find('motionDetection').text = 'false'

		if prefix + 'alerts-on' in data:
			if data[prefix + 'alerts-on'] in ('on', 'true'):
				camera.find('alerts').text = 'true'
			else:
				camera.find('alerts').text = 'false'

		camera.find('recordTime').text = str(data[prefix + 'rec-time'])

		self.file.write(CONF_FILE)	