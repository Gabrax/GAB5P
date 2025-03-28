const rl = @cImport(@cInclude("raylib.h"));

pub fn main() void {
    rl.InitWindow(800, 600, "Zig + Raylib");
    defer rl.CloseWindow();

    while (!rl.WindowShouldClose()) {
        rl.BeginDrawing();
        defer rl.EndDrawing();
        rl.ClearBackground(rl.RAYWHITE);
        rl.DrawText("Object Collision", 200, 600, 20, rl.DARKGRAY);
        rl.DrawText("Object Collision SAT!", 200, 500, 20, rl.DARKGRAY);
        rl.DrawText("Space Shooter!", 200, 400, 20, rl.DARKGRAY);
        rl.DrawText("Terrain Generator!", 200, 300, 20, rl.DARKGRAY);
        rl.DrawText("HexGrid!", 200, 200, 20, rl.DARKGRAY);
    }
}
