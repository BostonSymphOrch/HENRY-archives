var _gaq = _gaq || [];
_gaq.push(['_setAccount', 'UA-1852385-1']);
_gaq.push(['_setCustomVar', 1, 'Login status', '<%= loginStatus %>', 2]);
_gaq.push(['_setCustomVar', 1, 'key', '<%= sessionStateId %>', 2]);
_gaq.push(['_setDomainName', 'bso.org']);
_gaq.push(['_trackPageview']);
(function () {
    var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
    ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
    var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
})();