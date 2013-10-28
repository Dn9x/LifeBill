function getUrlPara(paraName) {
    var sUrl = location.href;
    var sReg = "(?:\\?|&){1}" + paraName + "=([^&]*)"
    var re = new RegExp(sReg, "gi");
    re.exec(sUrl);
    return RegExp.$1;
}