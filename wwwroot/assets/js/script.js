function passReveal(element) {
  //   var x = document.getElementById("inputPassword");
  let x = element;
  if (x.type === "password") {
    x.type = "text";
  } else {
    x.type = "password";
  }
}
