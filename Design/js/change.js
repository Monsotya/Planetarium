
function change_lang(obj) {
    /*let old = $("nav .lang span[toggle]");
    old.removeAttr("toggle");
    old.next().attr("toggle")*/
    let spans = obj.querySelectorAll("span");
    // console.log(spans);
    // let index = obj.querySelector("span[toggle]").indexOf();
    // let index = spans.indexOf(obj.querySelector("span[toggle]"));
    let index = Array.from(spans).findIndex(element => element.hasAttribute("toggle"))
    // console.log(index);
    spans[index].removeAttribute("toggle");
    index = (index === spans.length - 1) ? 0 : index + 1;
    spans[index].setAttribute("toggle", null);

}