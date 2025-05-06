from PIL import Image, ImageDraw
from math import sqrt

def lerp4(a, b, factor):
    return (a[0] * (1 - factor) + b[0] * factor, a[1] * (1 - factor) + b[1] * factor, a[2] * (1 - factor) + b[2] * factor, a[3] * (1 - factor) + b[3] * factor)

def hex_to_tuple(hex_code : str):
    hex_code = hex_code.lstrip('#')
    mmm = []
    for i in range(0, int(len(hex_code) / 2)):
        mmm.append(int(hex_code[i*2:i*2+2], 16))
    return tuple(mmm)

W = 200
H = 50

A_INPUT = "#ff0000FF"

B_INPUT = "#ff000000"

a = hex_to_tuple(A_INPUT)
b = hex_to_tuple(B_INPUT)

aRoot = (sqrt(a[0] / 255), sqrt(a[1] / 255), sqrt(a[2] / 255), sqrt(a[3] / 255))
bRoot = (sqrt(b[0] / 255), sqrt(b[1] / 255), sqrt(b[2] / 255), sqrt(b[3] / 255))

image = Image.new('RGBA', (W, H), color = (0,0,0,0))
draw = ImageDraw.Draw(image)

for i in range(0, W):
    factor = i / (W-1)
    resRoot = lerp4(aRoot, bRoot, factor)
    resColor = (int(resRoot[0] ** 2 * 255), int(resRoot[1] ** 2 * 255), int(resRoot[2] ** 2 * 255), int(resRoot[3] ** 2 * 255))
    draw.rectangle((i, 0, i+1, H / 2 - 1), fill = resColor)

    resColorNoSqrt = lerp4(a, b, factor)
    resColorNoSqrt = (int(resColorNoSqrt[0]), int(resColorNoSqrt[1]), int(resColorNoSqrt[2]), int(resColorNoSqrt[3]))
    draw.rectangle((i, H / 2, i+1, H - 1), fill = resColorNoSqrt)

image.save('example_image.png')

