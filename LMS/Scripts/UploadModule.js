(function () {
	var uMod = angular.module('UploadModule', []);
	uMod.controller('UploadController', ['$scope', function ($scope) {
		$scope.Model = {};

		$scope.initModel = function (KlassId) {
			$scope.Model.KlassID = KlassId;
			$scope.Model.ContentType = null;
			$scope.Model.Content = null;
			$scope.Model.FileName = null;
		};

		$scope.GetUpload= function () {
			var f = document.getElementById('file').files[0];
			$scope.Model.FileName = f.name;
			$scope.Model.ContentType = f.type;
			var r = new FileReader();
			r.onloadend = function (e) {
				$scope.Model.Content = e.target.result;
				console.log($scope.Model)
			};
			r.readAsArrayBuffer(f);
		};
	}]);

	var shuMod = angular.module('SharedUploadModule', ['UploadModule']);
	shuMod.controller('SharedUploadController', ['$scope', function ($scope) {
		/*
		$scope.Upload = function () {
			http.post("./Shared/Upload?Id=" + $scope.KlassId)
		}*/
	}]);

	var suuMod = angular.module('SubmitUploadModule', ['UploadModule']);
	suuMod.controller('SubmitUploadController', ['$scope', function ($scope) {
		/*
		$scope.Upload = function () {
			http.post("./Shared/Upload?Id=" + $scope.KlassId)
		}*/
	}]);
}());