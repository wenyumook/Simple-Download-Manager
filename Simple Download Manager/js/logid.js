function l() {
    function e(e) {
        if (e.length < 2) {
            var t = e.charCodeAt(0);
            return 128 > t ? e : 2048 > t ? d(192 | t >>> 6) + d(128 | 63 & t) : d(224 | t >>> 12 & 15) + d(128 | t >>> 6 & 63) + d(128 | 63 & t);
        }
        var i = 65536 + 1024 * (e.charCodeAt(0) - 55296) + (e.charCodeAt(1) - 56320);
        return d(240 | i >>> 18 & 7) + d(128 | i >>> 12 & 63) + d(128 | i >>> 6 & 63) + d(128 | 63 & i);
    }

    function t(t) {
        return (t + "" + Math.random()).replace(l, e);
    }

    function i(e) {
        var t = [0, 2, 1][e.length % 3],
            i = e.charCodeAt(0) << 16 | (e.length > 1 ? e.charCodeAt(1) : 0) << 8 | (e.length > 2 ? e.charCodeAt(2) : 0);
        return [o.charAt(i >>> 18), o.charAt(i >>> 12 & 63), t >= 2 ? "=" : o.charAt(i >>> 6 & 63), t >= 1 ? "=" : o.charAt(63 & i)].join("");
    }

    function n(e) {
        return e.replace(/[\s\S]{1,3}/g, i);
    }

    function a() {
        return n(t((new Date).getTime()));
    }

    var o = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/~！@#￥%……&",
        l = /[\uD800-\uDBFF][\uDC00-\uDFFFF]|[^\x00-\x7F]/g, d = String.fromCharCode;
    this.r = function (e, t) {
        return t ? a(String(e)).replace(/[+\/]/g, function (e) {
            return "+" == e ? "-" : "_";
        }).replace(/=/g, "") : a(String(e));
    };
}

var r = new l();
return r.r("BAIDUID");
