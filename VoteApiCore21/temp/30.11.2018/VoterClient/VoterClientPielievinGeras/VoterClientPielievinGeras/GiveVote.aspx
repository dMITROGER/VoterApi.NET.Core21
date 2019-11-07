﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GiveVote.aspx.cs" Inherits="VoterClientPielievinGeras.GiveVote" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
   
    <link rel="stylesheet" type="text/css" href="http://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.0/css/bootstrap.css"/>
    <style>
        .text-center {
         text-align:center;
        }
        .btn {
          margin:5px;
        }
        #chart_div:first-child {
                width: 100% !important;
          height: 100% !important;
        }
        .btn {
           min-width:150px
        }
        #candidatesAjax .btn {
            min-width:200px
        }
    </style>
    <meta http-equiv="Cache-Control" content="no-cache, no-store, must-revalidate" />
<meta http-equiv="Pragma" content="no-cache" />
<meta http-equiv="Expires" content="0" />
    <%--<meta http-equiv="refresh" content="10" />--%> 
</head>
<body>
   <div class="container" > 
        <div class="row">
           <div class="col-lg-12"> 
               <form id="Form1" runat="server">
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click1" Text="Button" />
                    <asp:Literal ID="ltScripts" runat="server"></asp:Literal> 
                    <div id="chart_div">  
                       
                    </div>  
                </form>
            </div>
        </div>
       
        <div class="row text-center"> 
            <div id="candidatesAjax" class="col-lg-12" >
            </div>
        </div>
        <hr />
        <div class="row text-center">  
            <a href="AddCandidate.aspx"  class="btn btn-sm btn-success"  > Candidates page</a> 
        </div>
        <br />
        <br />
        <br />
        <br />        
    </div>
    <script src="Scripts/jquery-3.3.1.min.js"></script>
    <script src="Scripts/jquery.unobtrusive-ajax.min.js"></script>
    <script type="text/javascript">
            function RefreshPage()
            {
             window.location.reload(true)
  }
    </script>

    <script>

        $(document).ready(function () {
            
            
            var uri = 'https://localhost:44388/api/Candidates';
            //var uri = 'http://pielievingerasapivoter-dev.us-east-2.elasticbeanstalk.com/api/Candidates';
            $.getJSON(uri)
                .done(function (data) {
                    $.each(data, function (key, item) {
                        
                        var val = '<button type="button" class="vote btn btn-sm btn-primary"  data-id="' + item.id + '">' + item.name + '</button>';
                        $('#candidatesAjax').append(val);
                        
                    });
                    
                    $('.vote').click(function () {
                        var candidate_id = $(this).data('id');
                        var uri = "https://localhost:44388/api/votes";
                        //var uri = "http/://pielievingerasapivoter-dev.us-east-2.elasticbeanstalk.com/api/votes";
                        var datas = "{id : 1, candidate_id: "+candidate_id+", user_id : 1}";
                        //var uri = "GiveVote.aspx/SendVote";
                        $.ajax({
                            url: uri,
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            data:  datas,
                            success : function(json) {
                                var json = JSON.parse(JSON.stringify(eval("(" + json + ")")), "'");
                                setChart(json);
                            },
                            error : function(xhr, status) {
                               // alert('Something went wrong' + xhr.responseText);
                                 var json = JSON.parse(JSON.stringify(eval("(" + xhr.responseText + ")")), "'");
                                setChart(json);
                            }
                        });
                    });
                });
            
        }); 
        function RefreshPage()
         {
             window.location.reload()
         }
    </script>
</body>
</html>