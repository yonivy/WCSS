#!C:\Python27\python.exe

import sys
import smtplib  
from email.mime.text import MIMEText

import config
from os.path import dirname, join
import xml.etree.ElementTree as ET
from xml.dom import minidom

ALERTS_FILE = join(dirname(__file__), 'data', 'alerts.xml')


def save_alert(cam_name, time, file_name):

	""" Save alert to alert.xml """

	alerts_file = ET.parse(ALERTS_FILE)
	root = alerts_file.getroot()

	last_id = 0
	for alert in root.findall('alert'):
		last_id = int(alert.get('id'))

	new_id = last_id + 1

	alert = ET.Element('alert')
	alert.set('id', str(new_id))
	alert.set('read', 'false')
	alert.set('status', 'notready') # status options: ready, notready

	a_time = ET.SubElement(alert, 'time')
	a_time.text = str(time)

	camera = ET.SubElement(alert, 'camera')
	camera.text = str(cam_name)

	link = ET.SubElement(alert, 'link')

	conf_obj = config.Config()
	lserver = conf_obj.get_lserver()

	l = "%s:%s/watchalert?vid=%s.mp4" % (lserver['url'], lserver['port'], file_name[:-4])

	link.text = l

	root.append(alert)

	alerts_file.write(ALERTS_FILE)

	return new_id

def get_alerts():

	""" Get every alert in alert.xml """

	alerts_file = ET.parse(ALERTS_FILE)
	root = alerts_file.getroot()

	alerts = []

	for a in root.findall('alert'):
		
		if a.get('status') == 'ready':
			alert = {}

			alert['id'] = a.get('id')
			alert['read'] = a.get('read')
			alert['status'] = a.get('status')

			dt = a.findtext('time').split('_')

			alert['date'] = dt[0].replace("-", "/")
			alert['time'] = dt[1].replace("-", ":")
			alert['camera'] = a.findtext('camera')
			alert['link'] = a.findtext('link')

			alerts.append(alert)

	return alerts

def delete_alerts(ids):

	""" Delete alerts from alert.xml """

	alerts_file = ET.parse(ALERTS_FILE)
	root = alerts_file.getroot()

	for id in ids:
		alert = root.find("./alert[@id='" + id + "']")
		root.remove(alert)

	alerts_file.write(ALERTS_FILE)	

def get_alert_video(id):

	""" Get alert video file path from alert.xml """

	alerts_file = ET.parse(ALERTS_FILE)
	root = alerts_file.getroot()	

	alert = root.find("./alert[@id='" + id + "']")
	return alert.findtext('link')


def set_read(alerts, is_read):

	""" Mark alert as read """

	alerts_file = ET.parse(ALERTS_FILE)
	root = alerts_file.getroot()	

	for a_id in alerts:
		alert = root.find("./alert[@id='" + a_id + "']")
		alert.set('read', str(is_read))

	alerts_file.write(ALERTS_FILE)

def set_status(alerts, status):

	""" Set alert staus (ready, notready) for reading """

	alerts_file = ET.parse(ALERTS_FILE)
	root = alerts_file.getroot()	

	for a_id in alerts:
		alert = root.find("./alert[@id='" + a_id + "']")
		alert.set('status', str(status))

	alerts_file.write(ALERTS_FILE)