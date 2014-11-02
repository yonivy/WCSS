package com.wbss.app;

import android.app.Activity;
import android.os.Bundle;
import android.widget.MediaController;
import android.widget.VideoView;

import com.wbss.model.Alert;
import com.yonivy.wbss.R;

public class AlertVideoActivity extends Activity {

	private VideoView videoPlayerView;
	private MediaController mediaController;

	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_alert_video);

		Alert alert = (Alert) getIntent().getSerializableExtra("alertObj");

		videoPlayerView = (VideoView) findViewById(R.id.video_player_view);
		videoPlayerView.setVideoPath(alert.getVideoURL());

		mediaController = new MediaController(this);
		videoPlayerView.setMediaController(mediaController);

		videoPlayerView.requestFocus();
		videoPlayerView.start();

	}

}
