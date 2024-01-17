import pygame
import sys
import random


pygame.init()


width, height = 800, 600
screen = pygame.display.set_mode((width, height))
pygame.display.set_caption("Detecting Collisions")


white = (255, 255, 255)
black = (0, 0, 0)
red = (255, 0, 0)


player_size = 50
player_x = width // 2 - player_size // 2
player_y = height - player_size - 10
player_speed = 5


obstacle_size = 50
obstacle_speed = 5
obstacle_frequency = 25
obstacles = []


triangle_size = 50
triangle_speed = 5
triangle_frequency = 50
triangles = []


clock = pygame.time.Clock()


def sat_rect(rect1, rect2):
    return not (
        rect1[0] + rect1[2] < rect2[0]
        or rect2[0] + rect2[2] < rect1[0]
        or rect1[1] + rect1[3] < rect2[1]
        or rect2[1] + rect2[3] < rect1[1]
    )


def sat_rect_triangle(rect, triangle):
    rect_points = [
        (rect[0], rect[1]),
        (rect[0] + rect[2], rect[1]),
        (rect[0], rect[1] + rect[3]),
        (rect[0] + rect[2], rect[1] + rect[3]),
    ]

    
    for i in range(4):
        edge = (
            rect_points[i],
            rect_points[(i + 1) % 4],
        )
        axis = (-edge[1][1] + edge[0][1], edge[1][0] - edge[0][0])

        min_proj_rect = float("inf")
        max_proj_rect = float("-inf")
        min_proj_triangle = float("inf")
        max_proj_triangle = float("-inf")

        
        for point in rect_points:
            proj = axis[0] * point[0] + axis[1] * point[1]
            min_proj_rect = min(min_proj_rect, proj)
            max_proj_rect = max(max_proj_rect, proj)

        
        for point in triangle:
            proj = axis[0] * point[0] + axis[1] * point[1]
            min_proj_triangle = min(min_proj_triangle, proj)
            max_proj_triangle = max(max_proj_triangle, proj)

        
        if (
            max_proj_triangle < min_proj_rect
            or max_proj_rect < min_proj_triangle
        ):
            return False

    return True


while True:
    for event in pygame.event.get():
        if event.type == pygame.QUIT:
            pygame.quit()
            sys.exit()

    keys = pygame.key.get_pressed()
    player_x += (keys[pygame.K_RIGHT] - keys[pygame.K_LEFT]) * player_speed
    player_y += (keys[pygame.K_DOWN] - keys[pygame.K_UP]) * player_speed

    
    if random.randint(1, obstacle_frequency) == 1:
        obstacle_x = random.randint(0, width - obstacle_size)
        obstacle_y = -obstacle_size
        obstacles.append([obstacle_x, obstacle_y, obstacle_size, obstacle_size, black])

    
    if random.randint(1, triangle_frequency) == 1:
        triangle_x = random.randint(0, width - triangle_size)
        triangle_y = -triangle_size
        triangles.append(
            [
                (triangle_x + triangle_size // 2, triangle_y),
                (triangle_x, triangle_y + triangle_size),
                (triangle_x + triangle_size, triangle_y + triangle_size),
                black,
            ]
        )

    
    for obstacle in obstacles:
        obstacle[1] += obstacle_speed

    
    for triangle in triangles:
        triangle[0] = (triangle[0][0], triangle[0][1] + triangle_speed)
        triangle[1] = (triangle[1][0], triangle[1][1] + triangle_speed)
        triangle[2] = (triangle[2][0], triangle[2][1] + triangle_speed)

    
    player_rect = pygame.Rect(player_x, player_y, player_size, player_size)
    for obstacle in obstacles:
        obstacle_rect = pygame.Rect(
            obstacle[0], obstacle[1], obstacle[2], obstacle[3]
        )
        if sat_rect(player_rect, obstacle_rect):
            obstacle[4] = red

   
    for triangle in triangles:
        if sat_rect_triangle(player_rect, triangle[:3]):
            triangle[3] = red

    
    screen.fill(white)
    pygame.draw.rect(screen, red, (player_x, player_y, player_size, player_size))

    for obstacle in obstacles:
        pygame.draw.rect(
            screen,
            obstacle[4],
            (obstacle[0], obstacle[1], obstacle[2], obstacle[3]),
        )

    for triangle in triangles:
        pygame.draw.polygon(screen, triangle[3], triangle[:3])  


    pygame.display.flip()

    
    clock.tick(60)
