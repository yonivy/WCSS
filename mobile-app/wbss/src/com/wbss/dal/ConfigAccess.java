package com.wbss.dal;

import java.io.IOException;
import java.io.UnsupportedEncodingException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.GregorianCalendar;

import org.apache.http.client.ClientProtocolException;
import org.apache.http.client.ResponseHandler;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.impl.client.BasicResponseHandler;
import org.apache.http.impl.client.DefaultHttpClient;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import com.wbss.app.WbssApplication;
import com.wbss.model.Camera;
import com.wbss.model.Md;

public class ConfigAccess {

	private String serverUrl;

	public ConfigAccess(WbssApplication app) {
		String localUrl = app.getLocalUrl();
		String localPort = app.getLocalPort();
		serverUrl = localUrl + ":" + localPort;
	}

	public void setMd(Md md) {

		DefaultHttpClient httpClient = new DefaultHttpClient();
		ResponseHandler<String> resonseHandler = new BasicResponseHandler();

		String url = serverUrl + "/set_md?md-on=" + md.isOn() + "&alerts-on=" + md.isAlertsOn();

		HttpGet getMethod = new HttpGet(url);

		try {
			getMethod.setHeader("Accept", "application/json");
			getMethod.setHeader("Content-Type", "application/json");

			String response = httpClient.execute(getMethod, resonseHandler);

		} catch (UnsupportedEncodingException e) {
			e.printStackTrace();
		} catch (ClientProtocolException e) {
			e.printStackTrace();
		} catch (IOException e) {
			e.printStackTrace();
		}
	}
	
	public Md getMd() {

		DefaultHttpClient httpClient = new DefaultHttpClient();
		ResponseHandler<String> resonseHandler = new BasicResponseHandler();

		String url = serverUrl + "/get_md";

		HttpGet getMethod = new HttpGet(url);

		Md md = null;
		
		try {
			getMethod.setHeader("Accept", "application/json");
			getMethod.setHeader("Content-Type", "application/json");

			String response = httpClient.execute(getMethod, resonseHandler);

			JSONObject json = new JSONObject(response);

			md = new Md(json.getBoolean("on"), json.getBoolean("alerts"));
				
		} catch (UnsupportedEncodingException e) {
			e.printStackTrace();
		} catch (ClientProtocolException e) {
			e.printStackTrace();
		} catch (IOException e) {
			e.printStackTrace();
		} catch (JSONException e) {
			e.printStackTrace();
		}
		
		return md;
	}
	
	public void setCamera(Camera camera) {

		DefaultHttpClient httpClient = new DefaultHttpClient();
		ResponseHandler<String> resonseHandler = new BasicResponseHandler();

		SimpleDateFormat sdf = new SimpleDateFormat("mm:ss");
		String time = sdf.format(camera.getRecordTime().getTime());

		String prefix = "cam-" + camera.getId() + "-";
		
		String url = serverUrl + "/setcam?id=" + camera.getId() + "&" + prefix + "md-on=" +
		camera.isMdOn() + "&" + prefix + "threshold=" + camera.getThreshold() +
		"&" + prefix + "alerts-on=" + camera.isAlertsOn() +
		"&" + prefix + "rec-time=" + time + "";

		HttpGet getMethod = new HttpGet(url);

		try {
			getMethod.setHeader("Accept", "application/json");
			getMethod.setHeader("Content-Type", "application/json");

			String response = httpClient.execute(getMethod, resonseHandler);

		} catch (UnsupportedEncodingException e) {
			e.printStackTrace();
		} catch (ClientProtocolException e) {
			e.printStackTrace();
		} catch (IOException e) {
			e.printStackTrace();
		}
	}

	public ArrayList<Camera> getCameras() {		

		ArrayList<Camera> cameras = new ArrayList<Camera>();

		DefaultHttpClient httpClient = new DefaultHttpClient();
		ResponseHandler<String> resonseHandler = new BasicResponseHandler();

		String url = serverUrl + "/getcams";

		HttpGet getMethod = new HttpGet(url);

		try {
			getMethod.setHeader("Accept", "application/json");
			getMethod.setHeader("Content-Type", "application/json");

			String response = httpClient.execute(getMethod, resonseHandler);

			JSONArray json = new JSONArray(response);

			for (int i = 0; i < json.length(); i++) {
				
				String[] rcTimeStr = json.getJSONObject(i).getString("rec_time").split(":");

				GregorianCalendar rcTime = new GregorianCalendar(0, 0, 0, 0,
						Integer.parseInt(rcTimeStr[0]), Integer.parseInt(rcTimeStr[1]));
				
				Camera cam = new Camera(
						json.getJSONObject(i).getInt("id"),
						json.getJSONObject(i).getString("name"), 
						json.getJSONObject(i).getInt("threshold"), 
						json.getJSONObject(i).getBoolean("md"),
						rcTime, 
						json.getJSONObject(i).getBoolean("alerts"),
						json.getJSONObject(i).getString("url"),
						json.getJSONObject(i).getString("port"), 
						json.getJSONObject(i).getString("stream"));

				cameras.add(cam);
			}

		} catch (UnsupportedEncodingException e) {
			e.printStackTrace();
		} catch (ClientProtocolException e) {
			e.printStackTrace();
		} catch (IOException e) {
			e.printStackTrace();
		} catch (JSONException e) {
			e.printStackTrace();
		}

		return cameras;

	}	

}
