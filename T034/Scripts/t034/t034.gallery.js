function  getWork(file, targetId, url) {
	$.ajax({
		url: url,
		data: { file: file }
	})
  .done(function (html) {
	  $(targetId).html(html);
  });
}

function showSample(file, targetId) {
    var originalFile = file.replace("-150.jpg", "-original.jpg").replace("-tn.jpg", ".jpg");

    $(targetId + ' a').each(function (index, element) {
        if (getFileName($(element).attr('href')) != originalFile)
            $(element).hide();
        else
            $(element).show();
    });
}

function showWork(file, targetId) {
    //var originalFile = file.replace("-150.jpg", "-original.jpg").replace("-tn.jpg", ".jpg");
    var originalFile = file.replace("-150.jpg", "-600.jpg").replace("-tn.jpg", ".jpg");

    $(targetId + ' .row').each(function (index, element) {
        if (getFileName($(element).data("original")) != originalFile)
            $(element).hide();
        else
            $(element).show();
    });
}

function getFileName(path) {
    return path.substring(path.lastIndexOf('/') + 1);
}

$('.gallery ul li img').click(function () {
    var src = this.src;
    var fileName = getFileName(src);
    if ($(this).data("src") != null)
        showWork(fileName, $(this).data("target"));
    else
        showSample(fileName, $(this).data("target"));
});