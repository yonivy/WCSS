package com.wbss.app;

import java.security.MessageDigest;
import java.util.List;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;

import com.parse.FindCallback;
import com.parse.Parse;
import com.parse.ParseException;
import com.parse.ParseObject;
import com.parse.ParseQuery;
import com.yonivy.wbss.R;

public class LoginActivity extends Activity {

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);

		setContentView(R.layout.activity_login);

		Parse.initialize(this, getResources().getString(R.string.parse_app_id), getResources()
				.getString(R.string.parse_client_key));

		Button loginBtn = (Button) findViewById(R.id.login_button);
		loginBtn.setOnClickListener(new Button.OnClickListener() {

			@Override
			public void onClick(View arg0) {
				EditText emailEditText = (EditText) findViewById(R.id.email_text);
				EditText passwordEditText = (EditText) findViewById(R.id.password_text);

				ParseQuery<ParseObject> query = ParseQuery.getQuery("LocalServer");
				query.whereEqualTo("email", emailEditText.getText().toString());
				String passwordHash = sha256(passwordEditText.getText().toString());
				query.whereEqualTo("password", passwordHash);

				query.findInBackground(new FindCallback<ParseObject>() {
					public void done(List<ParseObject> result, ParseException e) {
						if (e == null) {
							if (result.size() > 0) {
								String url = result.get(0).getString("url");
								String port = result.get(0).getString("port");

								((WbssApplication) LoginActivity.this.getApplication())
										.setLocalUrl(url);
								((WbssApplication) LoginActivity.this.getApplication())
										.setLocalPort(port);

								Intent i = new Intent(getBaseContext(), BaseActivity.class);
								startActivity(i);
							}
						}
					}
				});
			}
		});

	}

	private static String sha256(String base) {
		try {
			MessageDigest digest = MessageDigest.getInstance("SHA-256");
			byte[] hash = digest.digest(base.getBytes("UTF-8"));
			StringBuffer hexString = new StringBuffer();

			for (int i = 0; i < hash.length; i++) {
				String hex = Integer.toHexString(0xff & hash[i]);
				if (hex.length() == 1)
					hexString.append('0');
				hexString.append(hex);
			}

			return hexString.toString();
		} catch (Exception ex) {
			throw new RuntimeException(ex);
		}
	}
}
