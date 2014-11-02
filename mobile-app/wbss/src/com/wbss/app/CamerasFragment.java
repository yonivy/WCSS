package com.wbss.app;

import java.util.ArrayList;

import com.wbss.dal.ConfigAccess;
import com.wbss.model.Camera;
import com.yonivy.wbss.R;

import android.support.v4.app.ListFragment;
import android.content.Intent;
import android.os.AsyncTask;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.ListView;

public class CamerasFragment extends ListFragment {
	private ArrayAdapter<String> camerasAdapter;
	
	@Override
	public void onActivityCreated(Bundle savedInstanceState) {
		super.onActivityCreated(savedInstanceState);
		
		new GetCamerasTask().execute();
	}
	
	
	@Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,Bundle savedInstanceState) {
        ViewGroup root = (ViewGroup) inflater.inflate(R.layout.fragment_cameras, null);
        
        return root;
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
			ArrayList<String> cameraNames = new ArrayList<String>();
			
			for(int i = 0; i < result.size(); i++) {
				cameraNames.add(result.get(i).getName());
			}
			
			camerasAdapter = new ArrayAdapter<String>(getActivity(), R.layout.camera_list_item, R.id.cam_name_text, cameraNames);
			setListAdapter(camerasAdapter);
			
			final ArrayList<Camera> results = result;
			getListView().setOnItemClickListener(new ListView.OnItemClickListener() {

				@Override
				public void onItemClick(AdapterView<?> arg0, View arg1, int position, long arg3) {
					Intent i = new Intent(getActivity(), CameraActivity.class);
					i.putExtra("cameraObj", results.get(position));
					startActivity(i);
				}
			});
		}
	}
}
