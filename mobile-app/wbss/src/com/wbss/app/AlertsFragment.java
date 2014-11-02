package com.wbss.app;

import java.util.ArrayList;

import android.support.v4.app.ListFragment;
import android.os.AsyncTask;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;

import com.wbss.adapter.AlertsArrayAdapter;
import com.wbss.dal.AlertsAccess;
import com.wbss.model.Alert;
import com.yonivy.wbss.R;

public class AlertsFragment extends ListFragment {

	private AlertsArrayAdapter alertsAdapter;
	private ArrayList<Alert> alerts;

	@Override
	public void onActivityCreated(Bundle savedInstanceState) {
		super.onActivityCreated(savedInstanceState);

		setHasOptionsMenu(true);

		new GetAlertsTask().execute();
	}

	@Override
	public void onCreateOptionsMenu(Menu menu, MenuInflater inflater) {
		inflater.inflate(R.menu.alerts, menu);
	}

	@Override
	public boolean onOptionsItemSelected(MenuItem item) {

		ArrayList<Alert> checked = new ArrayList<Alert>();

		switch (item.getItemId()) {
		case R.id.mark_read:
			for (Alert a : alerts) {
				if (a.isSelected()) {
					a.setRead(true);
					checked.add(a);
				}
			}
			new MarkAlertsReadTask().execute(checked);

			break;
		case R.id.mark_unread:
			for (Alert a : alerts) {
				if (a.isSelected()) {
					a.setRead(false);
					checked.add(a);
				}
			}
			new MarkAlertsUnreadTask().execute(checked);

			break;
		case R.id.delete:
			ArrayList<Alert> notSelected = new ArrayList<Alert>();

			for (Alert a : alerts) {
				if (a.isSelected()) {
					checked.add(a);
				} else {
					notSelected.add(a);
				}
			}

			alerts.clear();
			alerts.addAll(notSelected);
			new DeleteAlertsTask().execute(checked);

			break;
		default:
			break;
		}

		return true;
	}

	private class GetAlertsTask extends AsyncTask<Void, Void, ArrayList<Alert>> {

		@Override
		protected ArrayList<Alert> doInBackground(Void... params) {
			AlertsAccess aa = new AlertsAccess((WbssApplication) getActivity().getApplication());
			ArrayList<Alert> alerts = aa.getAlerts();
			return alerts;
		}

		@Override
		protected void onPostExecute(ArrayList<Alert> result) {
			alertsAdapter = new AlertsArrayAdapter(getActivity(), result);
			setListAdapter(alertsAdapter);

			alerts = result;
		}
	}

	private class MarkAlertsReadTask extends AsyncTask<ArrayList<Alert>, Void, Void> {

		@Override
		protected Void doInBackground(ArrayList<Alert>... params) {
			AlertsAccess aa = new AlertsAccess((WbssApplication) getActivity().getApplication());
			aa.markAlertsRead(params[0]);
			return null;
		}

		@Override
		protected void onPostExecute(Void result) {
			alertsAdapter.notifyDataSetChanged();
		}
	}

	private class MarkAlertsUnreadTask extends AsyncTask<ArrayList<Alert>, Void, Void> {

		@Override
		protected Void doInBackground(ArrayList<Alert>... params) {
			AlertsAccess aa = new AlertsAccess((WbssApplication) getActivity().getApplication());
			aa.markAlertsUnread(params[0]);
			return null;
		}

		@Override
		protected void onPostExecute(Void result) {
			alertsAdapter.notifyDataSetChanged();
		}
	}

	private class DeleteAlertsTask extends AsyncTask<ArrayList<Alert>, Void, Void> {

		@Override
		protected Void doInBackground(ArrayList<Alert>... params) {
			AlertsAccess aa = new AlertsAccess((WbssApplication) getActivity().getApplication());
			aa.deleteAlerts(params[0]);
			return null;
		}

		@Override
		protected void onPostExecute(Void result) {
			alertsAdapter.notifyDataSetChanged();
		}
	}
}
