module.exports = function (result, mathMl) {
    var mjAPI = require("mathjax-node");
    mjAPI.config({
        MathJax: {
            // traditional MathJax configuration
            extensions: "MathML/content-mathml.js"
        }
    });
    mjAPI.start();


    mjAPI.typeset({
        math: mathMl,
        format: "MathML", // or "inline-TeX", "MathML"
        svg: true,      // or svg:true, or html:true
    }, function (data) {
        result(/*error*/ null, data.svg);
    });
}
