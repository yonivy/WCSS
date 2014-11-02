$(function () {    

  $('#md-settings-form').on('submit', function (e) {
    e.preventDefault(); 
    $.ajax({
      type: 'POST',
      url: 'save',
      data: $('#md-settings-form').serialize() + '&what=md',
      success: function (data) {
        alert("Data saved");
      }
    });
  });

  $('.cam-form').on('submit', function (e) {
    e.preventDefault(); 

    var camformid = '#' + $(this).attr('id');
    var camid = camformid.split('-')[1];

    var blerg = $(camformid).serialize() + '&id=' + camid + '&what=cam';

    $.ajax({
      type: 'POST',
      url: 'save',
      data: $(camformid).serialize() + '&id=' + camid + '&what=cam',
      success: function (data) {
        alert(data);
      }
    });
  });

  $('.md-check').on('click', function (e) {

    var spanId = '#' + $(this).attr('id') + '-text';

    if(this.checked == true) {
      $(spanId).text("Motion Detection On")
    }
    else {
      $(spanId).text("Motion Detection Off")
    }      
  });

  $('.md-slider').bind('change', function (e) {
    e.preventDefault();

    var spanId = '#' + $(this).attr('id') + '-val';

    $(spanId).text($(this).val())
        
  });

  $('.alerts-check').on('click', function (e) {

    var spanId = '#' + $(this).attr('id') + '-text';

    if(this.checked == true) {
      $(spanId).text("Alerts On")
    }
    else {
      $(spanId).text("Alerts Off")
    }      
  });
});