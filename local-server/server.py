import cherrypy
import os
import mimetypes
from os import path
from config import Config
import alerts
import alerts
from jinja2 import Environment, FileSystemLoader
import json,httplib,urllib
import xml.etree.ElementTree as ET
from os.path import dirname, join

current_dir = path.dirname(path.abspath(__file__))

conf = Config()
lserver = conf.get_lserver()
port = int(lserver['port'])

cherrypy.config.update({'server.socket_host': '0.0.0.0',
                        'server.socket_port': port,
                       })

config = {
 '/': {
   'tools.staticdir.root': current_dir,
  },
}


class Root:

	""" Root of the server application """

	@cherrypy.expose
	def index(self):
		return

	@cherrypy.expose
	def monitor(self):

		""" send alive message """

		return 'OK'

	@cherrypy.expose
	def getcams(self):

		""" Get all the cameras """

		conf = Config()
		cameras = conf.get_cameras()
		
		return json.dumps(cameras)

	@cherrypy.expose
	def setcam(self, **kwargs):

		""" Set camera data """

		conf = Config()
		conf.set_camera(kwargs)
		
		return json.dumps(kwargs)

	@cherrypy.expose
	def get_user(self):

		""" Get user data """

		conf = Config()

		user = conf.get_user()

		return json.dumps(user)

	@cherrypy.expose
	def set_user(self, **kwargs):

		""" Get user data """

		conf = Config()
		conf.set_user(kwargs)

	@cherrypy.expose
	def get_md(self):

		""" Get md data """

		conf = Config()

		md = conf.get_md()

		return json.dumps(md)

	@cherrypy.expose
	def set_md(self, **kwargs):

		""" Set md data """

		conf = Config()
		conf.set_md(kwargs)

	@cherrypy.expose
	def getalerts(self):

		""" Get all the alerts """

		a = alerts.get_alerts()
		
		return json.dumps(a)

	@cherrypy.expose
	def mark_alerts_read(self, **kwargs):

		""" Set alerts status to 'read' """

		a_ids = kwargs.pop('alerts', [])
		alerts.set_read(a_ids, 'true')

	@cherrypy.expose
	def mark_alerts_unread(self, **kwargs):

		""" Set alerts status to 'unread' """

		a_ids = kwargs.pop('alerts', [])
		alerts.set_read(a_ids, 'false')

	@cherrypy.expose
	def delete_alerts(self, **kwargs):

		""" Delete alerts """
		
		a_ids = kwargs.pop('alerts', [])
		alerts.delete_alerts(a_ids)

	@cherrypy.expose
	def watchalert(self, vid):
		
		""" Stream video file """

		BASE_PATH = "recordings"
		video = join(dirname(__file__), 'recordings', vid)
		if video == None:
			return "no file specified!"
		if not os.path.exists(video):
			return "file not found!"
		f = open(video, 'rb')
		size = os.path.getsize(video)
		mime = mimetypes.guess_type(video)[0]
		print mime
		cherrypy.response.headers["Content-Type"] = mime
		cherrypy.response.headers["Content-Disposition"] = 'attachment; filename="%s"' % os.path.basename(video)
		cherrypy.response.headers["Content-Length"] = size

		BUF_SIZE = 1024 * 5

		def stream():
			data = f.read(BUF_SIZE)
			while len(data) > 0:
				yield data
				data = f.read(BUF_SIZE)

		return stream()
	watchalert._cp_config = {'response.stream': True}	

app = cherrypy.tree.mount(Root(), "/", config)
cherrypy.engine.start()
cherrypy.engine.block()