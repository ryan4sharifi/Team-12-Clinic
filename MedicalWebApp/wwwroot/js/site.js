document.addEventListener('DOMContentLoaded', function () {
    var navToggle = document.querySelector('.nav-toggle');
    var navigation = document.querySelector('.navigation');

    navToggle.addEventListener('click', function () {
        navigation.classList.toggle('active');
        navToggle.classList.toggle('active');
    });
});
