function s(j, r) {
    var a = [];
    var p = [];
    var o = "";
    var v = j.length;
    for (var q = 0; q < 256; q++) {
        a[q] = j.substr((q % v), 1).charCodeAt(0);
        p[q] = q
    }
    for (var u = q = 0; q < 256; q++) {
        u = (u + p[q] + a[q]) % 256;
        var t = p[q];
        p[q] = p[u];
        p[u] = t
    }
    for (var i = u = q = 0; q < r.length; q++) {
        i = (i + 1) % 256;
        u = (u + p[i]) % 256;
        var t = p[i];
        p[i] = p[u];
        p[u] = t;
        k = p[((p[i] + p[u]) % 256)];
        o += String.fromCharCode(r.charCodeAt(q) ^ k)
    }
    return o;
}

function a(e) {
    var t = void 0,
        i = void 0,
        n = void 0,
        a = void 0,
        o = void 0,
        r = void 0,
        l = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
    for (n = e.length, i = 0, t = ""; n > i;) {
        if (a = 255 & e.charCodeAt(i++), i == n) {
            t += l.charAt(a >> 2), t += l.charAt((3 & a) << 4), t += "==";
            break;
        }
        if (o = e.charCodeAt(i++), i == n) {
            t += l.charAt(a >> 2), t += l.charAt((3 & a) << 4 | (240 & o) >> 4), t += l.charAt((15 & o) << 2), t += "=";
            break;
        }
        r = e.charCodeAt(i++), t += l.charAt(a >> 2), t += l.charAt((3 & a) << 4 | (240 & o) >> 4), t += l.charAt((15 & o) << 2 | (192 & r) >> 6), t += l.charAt(63 & r);
    }
    return t;
}

return a(s('SIGN3', 'SIGN1'));