let pass = document.getElementById('myinput-pass');
let togpass =document.getElementById('toggle-pass');

function showhide(){

if(pass.type === 'password'){

pass.setAttribute('type','text');
togpass.classList.add('hide');

}

else{

pass.setAttribute('type','password');
togpass.classList.remove('hide');

}
}