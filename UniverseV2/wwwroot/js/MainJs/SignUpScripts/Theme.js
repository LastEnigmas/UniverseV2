$(document).ready( function () {
    let num=0;
    $("#changebtn").click(function () {
        num++;
        if (num>=3){
            num=0;
        }

        if(num==0){

            $("body").addClass("earthy");
            $("body").removeClass("mars moony");

            $("#haveaccountbtn").addClass("btn-outline-primary");
            $("#haveaccountbtn").removeClass("btn-outline-warning btn-outline-light");


            $("#signUptext").removeClass("text-warning text-light");
            $("#signUptext").addClass("text-primary");

            $("#formDiv").css("margin-top","1%");

            $("#signUpbtn").removeClass("btn-warning btn-dark");
            $("#signUpbtn").addClass("btn-primary");

            $("#adventxt").removeClass("text-warning text-light");
            $("#adventxt").addClass("text-primary");

            $("#changebtn").removeClass("btn-outline-warning btn-outline-light");
            $("#changebtn").addClass("btn-outline-primary");

        }
        if(num==1){
            $("body").addClass("moony");
            $("body").removeClass("mars earthy");

            $("#haveaccountbtn").addClass("btn-outline-light");
            $("#haveaccountbtn").removeClass("btn-outline-warning btn-outline-primary");

            $("#changebtn").removeClass("btn-outline-warning btn-outline-primary");
            $("#changebtn").addClass("btn-outline-light");


            $("#signUptext").addClass("text-light");
            $("#signUptext").removeClass("text-primary text-warning");

            $("#formDiv").css("margin-top","1%");

            $("#signUpbtn").addClass("btn-dark");
            $("#signUpbtn").removeClass("btn-primary btn-warning");

            $("#adventxt").removeClass("text-warning text-primary");
            $("#adventxt").addClass("text-light");

        }

        if (num==2){
            $("#changebtn").removeClass("btn-outline-light btn-outline-primary");
            $("#changebtn").addClass("btn-outline-warning");

            $("body").removeClass("earthy moony");
            $("body").addClass("mars");

            $("#haveaccountbtn").removeClass("btn-outline-primary btn-outline-light");
            $("#haveaccountbtn").addClass("btn-outline-warning");


            $("#signUptext").addClass("text-warning");
            $("#signUptext").removeClass("text-primary text-light");

            $("#formDiv").css("margin-top","1%");

            $("#signUpbtn").addClass("btn-warning");
            $("#signUpbtn").removeClass("btn-primary btn-dark");

            $("#adventxt").addClass("text-warning");
            $("#adventxt").removeClass("text-primary text-light");


        }




    });



    $("#signUpbtn").click(function () {
        let valid= '@(ViewBag.IsTrue)';

        let toastmessages=document.getElementById("liveToast");

        let toast=new bootstrap.Toast(toastmessages);
        toast.show();






    });




});

