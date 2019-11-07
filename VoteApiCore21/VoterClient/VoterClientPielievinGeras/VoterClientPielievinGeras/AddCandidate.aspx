﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddCandidate.aspx.cs" Inherits="VoterClientPielievinGeras.AddCandidate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="http://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.0/css/bootstrap.css"/>
    <style>
        .btn {
           min-width:150px
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container" > 
        <div class="row">  
              <div class="bs-example">
                <h2 style="text-align:center" >Candidates</h2>
                <hr />
                <div class="row"> 
                    <div class="col-md-4 col-md-offset-4"   >
                        <ul id="candidatesAjax" style="list-style: disc">
                        </ul>  
                    </div>
                </div>
                <hr /> 
                <div class="row">
                    <div class="col-lg-4 col-lg-offset-4" style="text-align:center" > 
                        
                            <div class="form-group"> 
                            <input type="text" class="form-control" id="candidate_name" placeholder="Candidate Name"/>
                            
                            </div> 
                                <button type="button" class="btn btn-primary "  value="Add" onclick="add();" >Add</button>
                             
                    </div>
               </div>
              </div> 
        </div>
        <hr />
        <div class="row text-center">  
            <a href="GiveVote.aspx"  class="btn btn-success"  > Voting results page</a>
        </div>
        <hr />
        <div class="row text-center"> 
            <button type="button" class="deleteAll btn btn-danger"  >Delete all candidates</button>
            <button type="button" class="delete btn btn-danger"  >Delete last added candidate</button> 
        </div>

        <br />
        <br />
        <div class="row">
                    <div class="col-lg-4 col-lg-offset-4" style="text-align:center" > 
                        
                            <div class="form-group"> 
                                <p> <strong>Update candidate's name :</strong></p>
                            <input type="text" class="form-control" id="candidate_name1" placeholder="Old Name"/>
                                <br/>
                                <input type="text" class="form-control" id="candidate_name2" placeholder="New Name"/>

                            
                            </div> 
                                <button type="button" class="btn btn-primary "  value="Update" onclick="update();" >Update Name</button>
                             
                    </div>
        <br />
        <br />
    </div>

    <script src="Scripts/jquery-3.3.1.min.js"></script>
    <script src="Scripts/jquery.unobtrusive-ajax.min.js"></script>
    <script>

        $(document).ready(function () {
            var uri = 'https://localhost:44388/api/candidates';
            //var uri = 'http://pielievingerasapivoter-dev.us-east-2.elasticbeanstalk.com/api/candidates';
            //var uri = 'http://dmitrogeras-eval-test.apigee.net/votesystem/api/candidates';
            $.getJSON(uri)
                .done(function (data) {
                    $.each(data, function (key, item) {
                        $('<li>', { text: item.name, value: item.id }).appendTo($('#candidatesAjax'));
                    });
                });

            $('.delete').click(function () {
                var uri = 'https://localhost:44388/api/candidates/GetDeleteLast';
                //var uri = 'http://pielievingerasapivoter-dev.us-east-2.elasticbeanstalk.com/api/candidates/GetDeleteLast';
                //var uri = 'http://dmitrogeras-eval-test.apigee.net/votesystem/api/candidates/GetDeleteLast';
                $.ajax({
                    url: uri,
                    type: "DELETE",
                    success: function (result) {
                        location.reload();
                    },
                    error: function (xhr, status) {
                        alert('Something went wrong' + xhr.responseText);
                        location.reload();
                    }
                });
            });

            $('.deleteAll').click(function () {
                var uri = 'https://localhost:44388/api/candidates/GetDeleteAll';
                //var uri = 'http://pielievingerasapivoter-dev.us-east-2.elasticbeanstalk.com/api/candidates/GetDeleteAll';
                 //var uri = 'http://dmitrogeras-eval-test.apigee.net/votesystem/api/candidates/GetDeleteAll';
                $.ajax({
                    url: uri,
                    type: "DELETE",
                    success: function (result) {
                        location.reload();
                    },
                    error: function (xhr, status) {
                        alert('Something went wrong' + xhr.responseText);
                    }
                });
            });
        });

            function add() {
                var uri = 'https://localhost:44388/api/candidates';
                //var uri = 'http://pielievingerasapivoter-dev.us-east-2.elasticbeanstalk.com/api/candidates';
                //var uri = 'http://dmitrogeras-eval-test.apigee.net/votesystem/api/candidates';
                var datas = "{name :'" + $("#candidate_name").val() + "'}";
                $.ajax({
                    url: uri,
                    type: 'POST',
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    data: datas,
                    success: function (result) {
                        location.reload();
                    },
                    error: function (xhr, status) {
                        alert('Something went wrong' + xhr.responseText);
                    }
                });
            }

        function update() {
                
            var uri = "https://localhost:44388/api/candidates/" + $("#candidate_name2").val() + "";
            //var uri = "http://pielievingerasapivoter-dev.us-east-2.elasticbeanstalk.com/api/candidates/" + $("#candidate_name2").val() + "";
            //var uri = "http://dmitrogeras-eval-test.apigee.net/votesystem/api/candidates/" + $("#candidate_name2").val() + "";
                var datas = "{name :'" + $("#candidate_name1").val() + "'}";
                $.ajax({
                    url: uri,
                    type: 'PUT',
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    data: datas,
                    success: function (result) {
                        location.reload();
                    },
                    error: function (xhr, status) {
                        alert('Something went wrong' + xhr.responseText);
                    }
                });
        }
        

    </script>
        
    </form>
</body>
</html>
