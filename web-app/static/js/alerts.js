$(function () {
	$('#mark-read').on('click', function (e) {

		alert_ids = [];
		$("input:checkbox[name=alert-cb]:checked").each(function() {
		    alert_ids.push($(this).attr('id'));
		});
		
	    $.ajax({
	      type: 'GET',
	      url: 'markasread',
	      data: { alerts: alert_ids },
	      traditional: true,
	      success: function (data) {
	      	alert(data)	      	
	      	markRead();
	      }
	    });
	});

	$('#mark-unread').on('click', function (e) {

		alert_ids = [];
		$("input:checkbox[name=alert-cb]:checked").each(function() {
		    alert_ids.push($(this).attr('id'));
		});
		
	    $.ajax({
	      type: 'GET',
	      url: 'markasunread',
	      data: { alerts: alert_ids },
	      traditional: true,
	      success: function (data) {
	      	alert(data)	      	
	      	markUnread();
	      }
	    });
	});

	$('#delete').on('click', function (e) {

		alert_ids = [];
		$("input:checkbox[name=alert-cb]:checked").each(function() {
		    alert_ids.push($(this).attr('id'));
		});
		
	    $.ajax({
	      type: 'GET',
	      url: 'delete',
	      data: { alerts: alert_ids },
	      traditional: true,
	      success: function (data) {
	      	removeDeleted();
	      }
	    });
	});
});

function markRead() {
	$("input:checkbox[name=alert-cb]:checked").each(function() {
	    $(this).closest('tr').addClass('info');
	});
}

function markUnread() {
	$("input:checkbox[name=alert-cb]:checked").each(function() {
	    $(this).closest('tr').removeClass('info');
	});
}

function removeDeleted() {
	$("input:checkbox[name=alert-cb]:checked").each(function() {
	    $(this).closest('tr').remove();
	});
}