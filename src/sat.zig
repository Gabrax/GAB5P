const rl = @cImport(@cInclude("raylib.h"));
const std = @import("std");

const Point = struct {
    x: i32,
    y: i32,

    // Constructor-like function
    pub fn init(x: i32, y: i32) Point {
        return Point{ .x = x, .y = y };
    }

    // Function to calculate distance
    pub fn distance(self: *Point) f64 {
        const distance_squared = f64(self.x * self.x + self.y * self.y);
        return std.math.sqrt(distance_squared);
    }
};

// pub fn main() void {
//     var p = Point.init(3, 4);
//     std.debug.print("Distance from origin: {}\n", .{ p.distance() });
// }
