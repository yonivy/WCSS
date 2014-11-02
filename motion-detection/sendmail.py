#!C:\Python27\python.exe

import sys
sys.path.append("..")
import json, httplib
from server import config

""" Send an alerts mail to the user """

cam_name = str(sys.argv[1])

conf = config.Config()
user = conf.get_user()
mail = user['email']

connection = httplib.HTTPSConnection('api.parse.com', 443)
connection.connect()
connection.request('POST', '/1/functions/sendEmailAlert', json.dumps({
		"email": mail,
		"camera": cam_name
     }), {
       "X-Parse-Application-Id": "rrXt4pI1S7jo2E4kezSzmDce3CGd4EZbFVas7CM8",
       "X-Parse-REST-API-Key": "s0c9Xp2ZX5AoCe3T95PIQ5OTYj8XOR8C2ug29lQU",
       "Content-Type": "application/json"
     })
result = json.loads(connection.getresponse().read())
print result