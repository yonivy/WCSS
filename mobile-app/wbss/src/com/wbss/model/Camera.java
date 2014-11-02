package com.wbss.model;

import java.io.Serializable;
import java.util.GregorianCalendar;

public class Camera implements Serializable {
	private int id;
	private String name;
	private int threshold;
	private boolean mdOn;
	private boolean alertsOn;
	private GregorianCalendar recordTime;

	private String url;
	private String port;
	private String stream;

	public Camera(int id, String name, int threshold, boolean mdOn, GregorianCalendar recordTime,
			boolean alertsOn, String url, String port, String stream) {
		super();
		this.id = id;
		this.name = name;
		this.threshold = threshold;
		this.mdOn = mdOn;
		this.alertsOn = alertsOn;
		this.recordTime = recordTime;
		this.url = url;
		this.port = port;
		this.stream = stream;
	}

	public Camera(int id, String name, int threshold, boolean mdOn, GregorianCalendar recordTime) {
		super();
		this.id = id;
		this.name = name;
		this.threshold = threshold;
		this.mdOn = mdOn;
		this.recordTime = recordTime;
	}

	public int getId() {
		return id;
	}

	public void setId(int id) {
		this.id = id;
	}

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}

	public int getThreshold() {
		return threshold;
	}

	public void setThreshold(int threshold) {
		this.threshold = threshold;
	}

	public boolean isMdOn() {
		return mdOn;
	}

	public void setMdOn(boolean mdOn) {
		this.mdOn = mdOn;
	}

	public boolean isAlertsOn() {
		return alertsOn;
	}

	public void setAlertsOn(boolean alertsOn) {
		this.alertsOn = alertsOn;
	}

	public GregorianCalendar getRecordTime() {
		return recordTime;
	}

	public void setRecordTime(GregorianCalendar recordTime) {
		this.recordTime = recordTime;
	}

	public String getUrl() {
		return url;
	}

	public void setUrl(String url) {
		this.url = url;
	}

	public String getPort() {
		return port;
	}

	public void setPort(String port) {
		this.port = port;
	}

	public String getStream() {
		return stream;
	}

	public void setStream(String stream) {
		this.stream = stream;
	}
}
