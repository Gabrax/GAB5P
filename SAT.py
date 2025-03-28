from ursina import *
import random

app = Ursina()

window.title = "Detecting Collisions"
window.width = 800
window.height = 600

white = color.white
black = color.black
red = color.red

player_size = 0.5
player_speed = 0.05

obstacle_size = 0.5  # Adjust the size to make it more visible
obstacle_speed = 0.05
obstacle_frequency = 25

triangle_size = 0.5  # Adjust the size to make it more visible
triangle_speed = 0.05
triangle_frequency = 50

obstacles = []
triangles = []

player = Entity(model='quad', color=black, scale=(player_size, player_size, player_size), position=(0, -0.25, 0))

def sat_rect(rect1, rect2):
    return not (
        rect1.x + rect1.scale_x / 2 < rect2.x - rect2.scale_x / 2
        or rect2.x + rect2.scale_x / 2 < rect1.x - rect1.scale_x / 2
        or rect1.y + rect1.scale_y / 2 < rect2.y - rect2.scale_y / 2
        or rect2.y + rect2.scale_y / 2 < rect1.y - rect1.scale_y / 2
    )

# Function to detect collisions (Rectangle vs Triangle)
def sat_rect_triangle(rect, triangle):
    rect_points = [
        Vec2(rect.x - rect.scale_x / 2, rect.y - rect.scale_y / 2),
        Vec2(rect.x + rect.scale_x / 2, rect.y - rect.scale_y / 2),
        Vec2(rect.x - rect.scale_x / 2, rect.y + rect.scale_y / 2),
        Vec2(rect.x + rect.scale_x / 2, rect.y + rect.scale_y / 2)
    ]

    for i in range(4):
        edge = (rect_points[i], rect_points[(i + 1) % 4])
        axis = (-(edge[1].y - edge[0].y), edge[1].x - edge[0].x)

        min_proj_rect = float('inf')
        max_proj_rect = float('-inf')
        min_proj_triangle = float('inf')
        max_proj_triangle = float('-inf')

        for point in rect_points:
            proj = axis[0] * point.x + axis[1] * point.y
            min_proj_rect = min(min_proj_rect, proj)
            max_proj_rect = max(max_proj_rect, proj)

        for point in triangle:
            proj = axis[0] * point.x + axis[1] * point.y
            min_proj_triangle = min(min_proj_triangle, proj)
            max_proj_triangle = max(max_proj_triangle, proj)

        if max_proj_triangle < min_proj_rect or max_proj_rect < min_proj_triangle:
            return False

    return True

def update():
    global player


    if held_keys['right arrow']:
        player.x += player_speed
    if held_keys['left arrow']:
        player.x -= player_speed
    if held_keys['down arrow']:
        player.y -= player_speed
    if held_keys['up arrow']:
        player.y += player_speed

    if random.randint(1, obstacle_frequency) == 1:
        obstacle = Entity(model='quad', color=black, scale=(obstacle_size, obstacle_size, 0), position=(random.uniform(-0.35, 0.35), 0.5, 0))
        obstacles.append(obstacle)

    if random.randint(1, triangle_frequency) == 1:
        triangle = Entity(model='circle', color=black, scale=(triangle_size, triangle_size, 0), position=(random.uniform(-0.35, 0.35), 0.5, 0))
        triangles.append(triangle)

    for obstacle in obstacles:
        obstacle.y -= obstacle_speed
        if sat_rect(player, obstacle):
            obstacle.color = red

        if obstacle.y < -4:
            obstacles.remove(obstacle)

    for triangle in triangles:
        triangle.y -= triangle_speed
        if sat_rect_triangle(player, [triangle.position, Vec2(triangle.x - triangle_size/2, triangle.y - triangle_size), Vec2(triangle.x + triangle_size/2, triangle.y - triangle_size)]):
            triangle.color = red

        if triangle.y < -4:
            triangles.remove(triangle)


app.run()
