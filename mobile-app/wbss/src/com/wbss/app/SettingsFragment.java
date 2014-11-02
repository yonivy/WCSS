package com.wbss.app;

import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.GregorianCalendar;

import android.app.Activity;
import android.support.v4.app.Fragment;
import android.content.Context;
import android.os.AsyncTask;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.LinearLayout;
import android.widget.ListView;
import android.widget.SeekBar;
import android.widget.TextView;
import android.widget.ToggleButton;

import com.wbss.dal.ConfigAccess;
import com.wbss.model.Camera;
import com.wbss.model.Md;
import com.yonivy.wbss.R;

public class SettingsFragment extends Fragment {

	private ListView camerasListView;
	private CamerasAdapter camerasAdapter;

	private TextView nameTxt;
	private CheckBox md;
	private SeekBar mdThreshold;
	private CheckBox alertsOn;
	private EditText rcTime;

	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
		ViewGroup root = (ViewGroup) inflater.inflate(R.layout.fragment_settings, null);

		camerasListView = (ListView) root.findViewById(R.id.cameras_list);

		new GetCamerasTask().execute();

		return root;
	}

	public class CamerasAdapter extends ArrayAdapter<Camera> {

		private Context context;
		private ArrayList<Camera> cameras;

		public CamerasAdapter(Context context, ArrayList<Camera> objects) {
			super(context, R.layout.camera_settings_list_item, objects);

			this.context = context;
			this.cameras = objects;
		}

		@Override
		public View getView(int position, View convertView, ViewGroup parent) {

			if (convertView == null) {
				LayoutInflater inflater = ((Activity) context).getLayoutInflater();
				convertView = inflater.inflate(R.layout.camera_settings_list_item, parent, false);
			}

			Camera camera = cameras.get(position);

			nameTxt = (TextView) convertView.findViewById(R.id.name_text);
			md = (CheckBox) convertView.findViewById(R.id.md_checkBox);
			mdThreshold = (SeekBar) convertView.findViewById(R.id.md_threshold_seekBar);
			alertsOn = (CheckBox) convertView.findViewById(R.id.alerts_checkBox);
			rcTime = (EditText) convertView.findViewById(R.id.rc_editText);
			final Button saveBtn = (Button) convertView.findViewById(R.id.save_btn);
			saveBtn.setTag(camera);

			saveBtn.setOnClickListener(new Button.OnClickListener() {

				@Override
				public void onClick(View v) {
					LinearLayout parentView = (LinearLayout) saveBtn.getParent();
					Camera newCamera = (Camera) saveBtn.getTag();

					TextView nameTxt = (TextView) parentView.findViewById(R.id.name_text);

					CheckBox md = (CheckBox) parentView.findViewById(R.id.md_checkBox);

					SeekBar mdThreshold = (SeekBar) parentView
							.findViewById(R.id.md_threshold_seekBar);

					CheckBox alerts = (CheckBox) parentView.findViewById(R.id.alerts_checkBox);
					
					EditText rcTime = (EditText) parentView.findViewById(R.id.rc_editText);

					newCamera.setName(nameTxt.getText().toString());
					newCamera.setMdOn(md.isChecked());
					newCamera.setThreshold(mdThreshold.getProgress());
					newCamera.setAlertsOn(alerts.isChecked());

					String[] time = rcTime.getText().toString().split(":");

					GregorianCalendar recordTime = new GregorianCalendar(0, 0, 0, 0, Integer
							.parseInt(time[0]), Integer.parseInt(time[1]));

					newCamera.setRecordTime(recordTime);
					new SetCameraTask().execute(newCamera);
				}
			});

			nameTxt.setText(camera.getName());
			md.setChecked(camera.isMdOn());
			mdThreshold.setProgress(camera.getThreshold());
			alertsOn.setChecked(camera.isAlertsOn());

			SimpleDateFormat sdf = new SimpleDateFormat("mm:ss");
			String time = sdf.format(camera.getRecordTime().getTime());
			rcTime.setText(time);

			return convertView;
		}
	}

	private class GetCamerasTask extends AsyncTask<Void, Void, ArrayList<Camera>> {

		@Override
		protected ArrayList<Camera> doInBackground(Void... params) {
			ConfigAccess ca = new ConfigAccess((WbssApplication) getActivity().getApplication());
			ArrayList<Camera> cameras = ca.getCameras();
			return cameras;
		}

		@Override
		protected void onPostExecute(ArrayList<Camera> result) {
			camerasAdapter = new CamerasAdapter(getActivity(), result);
			camerasListView.setAdapter(camerasAdapter);
		}
	}

	private class SetCameraTask extends AsyncTask<Camera, Void, Void> {

		@Override
		protected Void doInBackground(Camera... params) {
			ConfigAccess ca = new ConfigAccess((WbssApplication) getActivity().getApplication());
			ca.setCamera(params[0]);
			return null;
		}

	}
	
}
