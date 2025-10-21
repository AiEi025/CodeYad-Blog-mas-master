$(document).ready(function () {
    LoadCkEditor4();
});

function LoadCkEditor4() {
    if (typeof CKEDITOR !== 'undefined') {
        // اگر از قبل لود شده است
        CKEDITOR.replace('ckEditor4', {
            customConfig: '/ckeditor4/ckeditor/config.js'
        });
        return;
    }

    // اگر هنوز لود نشده
    $.getScript('/ckeditor4/ckeditor/ckeditor.js', function () {
        CKEDITOR.replace('ckEditor4', {
            customConfig: '/ckeditor4/ckeditor/config.js'
        });
    });
}