package com.wbss.app;

import android.app.Activity;
import android.os.Bundle;
import android.webkit.WebView;

import com.wbss.model.Camera;
import com.yonivy.wbss.R;

public class CameraActivity extends Activity {

	private WebView webView;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_camera);

		webView = (WebView) findViewById(R.id.webView);
		webView.getSettings().setJavaScriptEnabled(true);

		Camera camera = (Camera) getIntent().getSerializableExtra("cameraObj");

		getActionBar().setTitle(camera.getName());
		
		String html = "<html><body><img src = \"" + camera.getUrl() + ":" + camera.getPort() + "/"
				+ camera.getStream() + "\"></body></html>";

		webView.loadData(html, "text/html", "UTF-8");
	}

}
