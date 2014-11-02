package com.wbss.dal;

import java.io.IOException;
import java.io.UnsupportedEncodingException;
import java.net.MalformedURLException;
import java.net.URL;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.GregorianCalendar;

import javax.xml.parsers.DocumentBuilder;
import javax.xml.parsers.DocumentBuilderFactory;
import javax.xml.parsers.ParserConfigurationException;

import org.apache.http.client.ClientProtocolException;
import org.apache.http.client.ResponseHandler;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.impl.client.BasicResponseHandler;
import org.apache.http.impl.client.DefaultHttpClient;
import org.json.JSONArray;
import org.json.JSONException;
import org.w3c.dom.Document;
import org.w3c.dom.Element;
import org.w3c.dom.Node;
import org.w3c.dom.NodeList;
import org.xml.sax.InputSource;
import org.xml.sax.SAXException;

import android.R.integer;
import android.os.StrictMode;

import com.wbss.app.LoginActivity;
import com.wbss.app.WbssApplication;
import com.wbss.model.Alert;

public class AlertsAccess {
	private String serverUrl;

	public AlertsAccess(WbssApplication app) {
		String localUrl = app.getLocalUrl();
		String localPort = app.getLocalPort();
		serverUrl = localUrl + ":" + localPort;
	}

	public ArrayList<Alert> getAlerts() {

		ArrayList<Alert> alerts = new ArrayList<Alert>();

		DefaultHttpClient httpClient = new DefaultHttpClient();
		ResponseHandler<String> resonseHandler = new BasicResponseHandler();

		String url = serverUrl + "/getalerts";

		HttpGet getMethod = new HttpGet(url);

		try {
			getMethod.setHeader("Accept", "application/json");
			getMethod.setHeader("Content-Type", "application/json");

			String response = httpClient.execute(getMethod, resonseHandler);

			JSONArray json = new JSONArray(response);

			for (int i = 0; i < json.length(); i++) {

				SimpleDateFormat sdf = new SimpleDateFormat("dd/MM/yyyy HH:mm:ss");
				String datetime = json.getJSONObject(i).getString("date") + " "
						+ json.getJSONObject(i).getString("time");

				GregorianCalendar cal = new GregorianCalendar();
				cal.setTime(sdf.parse(datetime));

				Alert alert = new Alert(
						json.getJSONObject(i).getInt("id"),
						json.getJSONObject(i).getBoolean("read"), 
						json.getJSONObject(i).getString("status"), 
						json.getJSONObject(i).getString("camera"), 
						cal, 
						json.getJSONObject(i).getString("link"));

				alerts.add(alert);
			}

		} catch (UnsupportedEncodingException e) {
			e.printStackTrace();
		} catch (ClientProtocolException e) {
			e.printStackTrace();
		} catch (IOException e) {
			e.printStackTrace();
		} catch (JSONException e) {
			e.printStackTrace();
		} catch (ParseException e) {
			e.printStackTrace();
		}

		return alerts;
	}

	public void markAlertsRead(ArrayList<Alert> alerts) {
		callAlertsAction("mark_alerts_read", alerts);
	}

	public void markAlertsUnread(ArrayList<Alert> alerts) {
		callAlertsAction("mark_alerts_unread", alerts);
	}

	public void deleteAlerts(ArrayList<Alert> alerts) {
		callAlertsAction("delete_alerts", alerts);
	}
	
	private void callAlertsAction(String action, ArrayList<Alert> alerts){
		
		DefaultHttpClient httpClient = new DefaultHttpClient();
		ResponseHandler<String> resonseHandler = new BasicResponseHandler();
		
		String getParams = "?";
		for (Alert alert : alerts) {
			getParams += "alerts=" + alert.getId() + "&";
		}

		getParams = getParams.substring(0, getParams.length() - 1);
		
		String url = serverUrl + "/" + action + getParams;

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
}
