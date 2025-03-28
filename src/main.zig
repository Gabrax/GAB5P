const c = @cImport(@cInclude("raylib.h"));

pub fn main() void {
    c.InitWindow(800, 600, "Zig + Raylib");
    defer c.CloseWindow();

    while (!c.WindowShouldClose()) {
        c.BeginDrawing();
        defer c.EndDrawing();
        c.ClearBackground(c.RAYWHITE);
        c.DrawText("Object Collision", 200, 600, 20, c.DARKGRAY);
        c.DrawText("Object Collision SAT!", 200, 500, 20, c.DARKGRAY);
        c.DrawText("Space Shooter!", 200, 400, 20, c.DARKGRAY);
        c.DrawText("Terrain Generator!", 200, 300, 20, c.DARKGRAY);
        c.DrawText("HexGrid!", 200, 200, 20, c.DARKGRAY);
    }
}
