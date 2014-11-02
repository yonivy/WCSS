import cherrypy
import sys
sys.path.append("..")
from server.config import Config
from server import alerts
from os import path
from jinja2 import Environment, FileSystemLoader
import json, httplib, urllib, urllib2
from auth import require
import hashlib

current_dir = path.dirname(path.abspath(__file__))
env = Environment(loader=FileSystemLoader(path.join(current_dir,'templates')))

conf = Config()
web_app_server = conf.get_web_app_server()
port = int(web_app_server['port'])

cherrypy.config.update({'server.socket_host': '0.0.0.0',
                        'server.socket_port': port, 
                       })

config = {
 '/': {
   'tools.staticdir.root': current_dir,
  },
 '/static': {
   'tools.staticdir.on': True,
   'tools.staticdir.dir': "static",
   },
}

SESSION_KEY = '_cp_username'
server_url = None

class Settings:
	
	""" Settings page of the application """

	@cherrypy.expose
	@require()
	def index(self):

		""" Index page """

		global server_url

		response = urllib2.urlopen(server_url + '/getcams')
		cams = json.loads(response.read())		

		response = urllib2.urlopen(server_url + '/get_md')
		md = json.loads(response.read())

		tmpl = env.get_template("settings.html")
		return tmpl.render(md=md, cams=cams)

	@cherrypy.expose
	@require()
	def save(self, **kwargs):

		""" Save changes in settings """

		global server_url

		params = urllib.urlencode(kwargs)

		if(kwargs['what'] == 'user'):
			response = urllib2.urlopen(server_url + '/set_user?' + params)
		elif(kwargs['what'] == 'md'):
			response = urllib2.urlopen(server_url + '/set_md?' + params)
		elif(kwargs['what'] == 'cam'):
			response = urllib2.urlopen(server_url + '/setcam?' + params)

class Alerts:

	""" Alerts page of the application """

	@cherrypy.expose
	@require()
	def index(self):

		""" Index page """
		
		global server_url

		response = urllib2.urlopen(server_url + '/getalerts')
		alerts = json.loads(response.read())

		tmpl = env.get_template("alerts.html")
		return tmpl.render(alerts=alerts)

	@cherrypy.expose
	@require()
	def delete(self, **alerts_ids):

		""" Delete an alert """

		global server_url

		ids = alerts_ids.pop('alerts', [])

		get_params = '?alerts=' + '&alerts='.join(ids)

		response = urllib2.urlopen(server_url + '/delete_alerts' + get_params)

		tmpl = env.get_template("alerts.html")
		return tmpl.render()

	@cherrypy.expose
	@require()	   
	def markasread(self, **alerts_ids):

		""" Mark alerts as read """

		global server_url

		ids = alerts_ids.pop('alerts', [])

		get_params = '?alerts=' + '&alerts='.join(ids)

		response = urllib2.urlopen(server_url + '/mark_alerts_read' + get_params)

	@cherrypy.expose
	@require()	   
	def markasunread(self, **alerts_ids):

		""" Mark alerts as read """

		global server_url

		ids = alerts_ids.pop('alerts', [])

		get_params = '?alerts=' + '&alerts='.join(ids)

		response = urllib2.urlopen(server_url + '/mark_alerts_unread' + get_params)

class Cameras:

	""" Cameras page of the application """

	@cherrypy.expose
	@require()
	def index(self):

		""" Index page """

		global server_url

		response = urllib2.urlopen(server_url + '/getcams')
		cams = json.loads(response.read())

		tmpl = env.get_template("cameras.html")
		return tmpl.render(cams=cams)

class Login:

	""" Login page of the application """

	@cherrypy.expose
	def index(self):
		tmpl = env.get_template("login.html")
		return tmpl.render()

	@cherrypy.expose
	def enter(self, email, password):

		""" Authenticate user against remote server """

		global server_url

		connection = httplib.HTTPSConnection('api.parse.com', 443)
		params = urllib.urlencode({"where":json.dumps({
		       "email": email,
		       "password": hashlib.sha256(password).hexdigest()
		     })})
		connection.connect()
		connection.request('GET', '/1/classes/LocalServer?%s' % params, '', {
		       "X-Parse-Application-Id": "rrXt4pI1S7jo2E4kezSzmDce3CGd4EZbFVas7CM8",
		       "X-Parse-REST-API-Key": "s0c9Xp2ZX5AoCe3T95PIQ5OTYj8XOR8C2ug29lQU"
		     })
		result = json.loads(connection.getresponse().read())
		

		if result['results']:
			server_url = "%s:%s" % ((result['results'][0])['url'], (result['results'][0])['port'])
			cherrypy.session[SESSION_KEY] = cherrypy.request.login = server_url
			raise cherrypy.HTTPRedirect("/cameras")
		else:
			msg = "Authentication failed"

			tmpl = env.get_template("login.html")
			return tmpl.render(error_msg=msg)

class Root:

	""" Root of the application """

	global server_url

	_cp_config = {
        'tools.sessions.on': True,
        'tools.auth.on': True
    }	

	login = Login()
	cameras = Cameras()
	alerts = Alerts()
	settings = Settings()

	@cherrypy.expose
	def index(self):
		raise cherrypy.HTTPRedirect("/login")		

app = cherrypy.tree.mount(Root(), "/", config)
cherrypy.engine.start()
cherrypy.engine.block()