package com.wbss.adapter;

import java.text.SimpleDateFormat;
import java.util.ArrayList;

import android.app.Activity;
import android.content.Intent;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.CompoundButton;
import android.widget.ImageButton;
import android.widget.TextView;

import com.parse.R.color;
import com.wbss.app.AlertVideoActivity;
import com.wbss.model.Alert;
import com.yonivy.wbss.R;

public class AlertsArrayAdapter extends ArrayAdapter<Alert> {
	private Activity context;
	private ArrayList<Alert> alerts;

	public AlertsArrayAdapter(Activity context, ArrayList<Alert> objects) {
		super(context, R.layout.alert_list_item, objects);

		this.context = context;
		this.alerts = objects;
	}

	static class ViewHolder {
		protected CheckBox checkbox;
		protected TextView camName;
		protected TextView time;
		protected ImageButton vidBtn;
	}

	@Override
	public View getView(int position, View convertView, ViewGroup parent) {

		View view = null;

		if (convertView == null) {
			LayoutInflater inflater = ((Activity) context).getLayoutInflater();
			view = inflater.inflate(R.layout.alert_list_item, parent, false);
			
			final ViewHolder viewHolder = new ViewHolder();

			viewHolder.checkbox = (CheckBox) view.findViewById(R.id.alert_cbx);
			viewHolder.camName = (TextView) view.findViewById(R.id.camera_text);
			viewHolder.time = (TextView) view.findViewById(R.id.time_text);
			viewHolder.vidBtn = (ImageButton) view.findViewById(R.id.video_btn);

			viewHolder.checkbox
					.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {

						@Override
						public void onCheckedChanged(CompoundButton buttonView, boolean isChecked) {
							Alert alert = (Alert) viewHolder.checkbox.getTag();
							alert.setSelected(buttonView.isChecked());
						}
					});

			view.setTag(viewHolder);
			viewHolder.checkbox.setTag(alerts.get(position));
		} else {
			view = convertView;
			((ViewHolder) view.getTag()).checkbox.setTag(alerts.get(position));
		}

		if (alerts.get(position).isRead())
			view.setBackgroundColor(context.getResources().getColor(R.color.alert_item_bg_read));
		else
			view.setBackgroundColor(context.getResources().getColor(R.color.alert_item_bg_unread));
		
		ViewHolder holder = (ViewHolder) view.getTag();

		holder.checkbox.setChecked(alerts.get(position).isSelected());

		holder.camName.setText(alerts.get(position).getCameraName());

		SimpleDateFormat sdf = new SimpleDateFormat("dd/MM/yyyy HH:mm:ss");
		String time = sdf.format(alerts.get(position).getTime().getTime());
		holder.time.setText(time);

		final int fposition = position;
		holder.vidBtn.setOnClickListener(new ImageButton.OnClickListener() {

			@Override
			public void onClick(View v) {
				Intent i = new Intent(context, AlertVideoActivity.class);
				i.putExtra("alertObj", alerts.get(fposition));
				context.startActivity(i);
			}
		});

		return view;

		/*
		 * final Alert alert = alerts.get(position);
		 * 
		 * TextView camTxt = (TextView)
		 * convertView.findViewById(R.id.camera_text); TextView timeTxt =
		 * (TextView) convertView.findViewById(R.id.time_text); Button vidBtn =
		 * (Button) convertView.findViewById(R.id.video_btn);
		 * 
		 * camTxt.setText(alert.getCameraName());
		 * 
		 * SimpleDateFormat sdf = new SimpleDateFormat("dd/MM/yyyy HH:mm:ss");
		 * String time = sdf.format(alert.getTime().getTime());
		 * timeTxt.setText(time);
		 * 
		 * vidBtn.setOnClickListener(new Button.OnClickListener() {
		 * 
		 * @Override public void onClick(View v) { Intent i = new
		 * Intent(context, VideoAlertActivity.class); i.putExtra("alertObj",
		 * alert); context.startActivity(i); } });
		 * 
		 * return convertView;
		 */
	}
}
