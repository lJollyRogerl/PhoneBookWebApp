window.onload = function () {
    //if when user has unloaded page - adding form was hidden - hide it and vice versa
    var isDivHidden = sessionStorage.getItem('isCreationDivHidden');
    if (isDivHidden != null)
        $('#addingForm').attr('class', isDivHidden);
    //if when user has unloaded page - there was some searching request - place it again
    var lastSearchingRequest = sessionStorage.getItem('wasSearching');
    if (lastSearchingRequest != null) {
        SearchingName(lastSearchingRequest);
        $('#txtSearch').val(lastSearchingRequest);
        sessionStorage.removeItem('wasSearching');
    }
}


window.onbeforeunload = function () {
    //adding some user settings before unload
    sessionStorage.setItem('isCreationDivHidden', $('#addingForm').attr('class'));
    var actualRequest = $('#txtSearch').val();
    if (actualRequest.length > 0) {
        sessionStorage.setItem('wasSearching', actualRequest);
    }
}



function Initializing() {
    //roll down adding form by clicking the anchor addNewContact
    $('#addNewContact').click(function (arg) {
        arg.preventDefault();
        $('#addingForm').toggleClass('shown hidden');
    });
    
    //current loaded page of users. At the beggining it will be 1
    var currentPage = 1;

    //if the user have reached the bottom of the page - load next
    //but if there is a search request has been typed in - do nothing 
    $(window).scroll(function () {
        if (($(window).scrollTop() == $(document).height() - $(window).height())
            && ($('#txtSearch').val().length <= 0)) {
            currentPage += 1;
            GetPage(currentPage);
        }
    });

    //invokes search mini-engine
    $('#txtSearch').keyup(function () {
        var pattern = $(this).val();
        //Сalling the function which search and render wanted matches
        SearchingName(pattern);
    });

    $('#upArrow').click(function () {
        $('html, body').animate({ scrollTop: 0 }, 'slow');
    });

    //if scroll bar reaches 400 px down - arrow will be shown and vice versa
    $(window).scroll(function () {
        if (scrollY > 400)
        { $('#upArrow').show(); }
        if (scrollY <= 400)
        { $('#upArrow').hide(); }
    });
}

//The function which updates the table body through ajax using the wanted name
//and showing the matches
function SearchingName(wantedName) {
    $.ajax({
        url: '/Home/GetCitizensByString',
        data: { txtSearch: wantedName },
        dataType: 'html',
        success: function (data) {
            $('#tableHeaders').show();
            $('#tbodyCitizens').html(data);
        },
        error: function (err) { alert('Введен недопустимый запрос'); }
    });
}

//returning a partion view which renders page of contacts and adding it to the list
function GetPage(pageToRender)
{
    $.ajax({
        url: '/Home/GetPageOfCitizens',
        data: { page: pageToRender },
        dataType: 'html',
        success: function (data) {
            $('#tbodyCitizens').append(data);
        },
        error: function (err) { alert('Ошибка загрузки страницы!'); }
    });
}

$(document).ready(function () {
    Initializing();
});
