#!C:\Python27\python.exe

import sys
sys.path.append("..")
import os
from subprocess import call
from server import alerts

alert_id = [str(sys.argv[1])]
old_file_name = str(sys.argv[2])
new_file_name = str(sys.argv[3])

call(['ffmpeg', '-i', old_file_name, '-c:v', 'libx264', '-preset', 'fast', '-profile:v', 'baseline', new_file_name])
os.remove(old_file_name)

alerts.set_status(alert_id, 'ready')