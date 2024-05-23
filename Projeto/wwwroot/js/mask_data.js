function mask(o,f){
    v_obj=o
    v_fun=f
    setTimeout("execmask()",1)
    }

    function execmask(){
    v_obj.value=v_fun(v_obj.value)
    }




function maskdata(v){

    v=v.replace(/\D/g,"");
    v=v.replace(/(\d{2})(\d)/,"$1/$2")
    v=v.replace(/(\d{2})(\d)/,"$1/$2")
    return v;

    }
