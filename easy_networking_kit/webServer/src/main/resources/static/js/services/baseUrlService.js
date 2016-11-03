(function () {
    var app = angular.module('EasyNetworking');

//Returns the url used for communication
app.service('urlService', function() {

		var baseUrl = 'http://localhost:8080/';

		var getBaseUrl = function() {

			return baseUrl;
		};

		return {
			getBaseUrl : getBaseUrl
		}

	});
})();