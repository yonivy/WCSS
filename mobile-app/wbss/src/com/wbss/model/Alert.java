package com.wbss.model;

import java.io.Serializable;
import java.util.GregorianCalendar;

public class Alert implements Serializable {
	private int id;
	private boolean read;
	private String status;
	private String cameraName;
	private GregorianCalendar time;
	private String videoURL;
	private boolean selected;

	public Alert(int id, boolean read, String status, String cameraName, GregorianCalendar time,
			String videoURL) {
		super();
		this.id = id;
		this.read = read;
		this.status = status;
		this.cameraName = cameraName;
		this.time = time;
		this.videoURL = videoURL;
	}

	public int getId() {
		return id;
	}

	public void setId(int id) {
		this.id = id;
	}

	public boolean isRead() {
		return read;
	}

	public void setRead(boolean read) {
		this.read = read;
	}

	public String getStatus() {
		return status;
	}

	public void setStatus(String status) {
		this.status = status;
	}

	public String getCameraName() {
		return cameraName;
	}

	public void setCameraName(String cameraName) {
		this.cameraName = cameraName;
	}

	public GregorianCalendar getTime() {
		return time;
	}

	public void setTime(GregorianCalendar time) {
		this.time = time;
	}

	public String getVideoURL() {
		return videoURL;
	}

	public void setVideoURL(String videoURL) {
		this.videoURL = videoURL;
	}

	public boolean isSelected() {
		return selected;
	}

	public void setSelected(boolean selected) {
		this.selected = selected;
	}

	// for debugging
	@Override
	public String toString() {
		return cameraName;
	}
}
