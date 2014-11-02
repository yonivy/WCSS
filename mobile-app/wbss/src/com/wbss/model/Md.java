package com.wbss.model;

public class Md {
	private boolean on;
	private boolean alertsOn;

	public Md(boolean on, boolean alertsOn) {
		super();
		this.on = on;
		this.alertsOn = alertsOn;
	}

	public boolean isOn() {
		return on;
	}

	public void setOn(boolean on) {
		this.on = on;
	}

	public boolean isAlertsOn() {
		return alertsOn;
	}

	public void setAlertsOn(boolean alertsOn) {
		this.alertsOn = alertsOn;
	}

}
