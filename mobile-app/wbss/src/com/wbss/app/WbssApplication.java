package com.wbss.app;

import android.app.Application;

public class WbssApplication extends Application {
	private String localUrl;// = "http://hs.duckdns.org";
	private String localPort;// = "8080";

	public String getLocalUrl() {
		return localUrl;
	}

	public void setLocalUrl(String localUrl) {
		this.localUrl = localUrl;
	}

	public String getLocalPort() {
		return localPort;
	}

	public void setLocalPort(String localPort) {
		this.localPort = localPort;
	}

}
