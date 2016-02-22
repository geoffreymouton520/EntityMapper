var gulp = require("gulp");
var gutil= require("gulp-util");
var inject = require("gulp-inject");

gulp.task("index", function () {
    var target = gulp.src("index.html");
    // It's not necessary to read the files (will speed up things), we're only after their paths:
    var sources = gulp.src(["Scripts/app/**/*.js", "Content/**/*.css"], { read: false });

    return target.pipe(inject(sources))
      .pipe(gulp.dest("/dest"));
});

gulp.task("default", ["index"],function() {
    gutil.log("Injected files");
});