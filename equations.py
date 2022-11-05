from math import cos, sin, tan, atan

EPS = 5 * 10e-6


def f(x):
    return tan(x) - 0.34 / x


def df(x):
    return 1.0 / (cos(x) * cos(x)) + 0.34 / (x * x)


def d2f(x):
    return 2.0 * sin(x) / pow(cos(x), 3.0) - 0.68 / pow(x, 3.0)


def phi(x):
    return atan(0.34 / x)


def select_start_point(a, b):
    if f(a) * d2f(a) > 0:
        return a
    elif f(b) * d2f(b) > 0:
        return b
    else:
        raise Exception("Something strange")


def dichotomy(a, b, is_increasing):
    counter = 0
    x_prev = a
    x_cur = b
    while abs(x_cur - x_prev) >= EPS:
        x_prev = x_cur
        x_cur = (a + b) / 2.0
        if is_increasing:
            if f(x_cur) >= 0:
                b = x_cur
            elif f(x_cur) < 0:
                a = x_cur
        else:
            if f(x_cur) >= 0:
                a = x_cur
            elif f(x_cur) < 0:
                b = x_cur
        counter += 1
    print(f"x = {x_cur}. Iterations: {counter}")


def newton(a, b):
    counter = 0
    x_prev = select_start_point(a, b)
    x_cur = b if x_prev == a else a
    while abs(x_cur - x_prev) >= EPS:
        x_prev = x_cur
        x_cur = x_cur - f(x_cur) / df(x_cur)
        counter += 1
    print(f"x = {x_cur}. Iterations: {counter}")


def modified_newton(a, b):
    counter = 0
    x_prev = select_start_point(a, b)
    dfx = df(x_prev)
    x_cur = b if x_prev == a else a
    while abs(x_cur - x_prev) >= EPS:
        x_prev = x_cur
        x_cur = x_cur - f(x_cur) / dfx
        counter += 1
    print(f"x = {x_cur}. Iterations: {counter}")


def chord(a, b):
    counter = 1
    x_start = select_start_point(a, b)
    x_cur = b if x_start == a else a
    x_next = x_cur - f(x_cur) / (f(x_cur) - f(x_start)) * (x_cur - x_start)
    while abs(x_cur - x_next) >= EPS:
        x_cur = x_next
        x_next = x_cur - f(x_cur) / (f(x_cur) - f(x_start)) * (x_cur - x_start)
        counter += 1
    print(f"x = {x_next}. Iterations: {counter}")


def movable_chord(a, b):
    counter = 0
    x_prev = select_start_point(a, b)
    x_cur = b if x_prev == a else a
    while abs(x_cur - x_prev) >= EPS:
        x_next = x_cur - f(x_cur) / (f(x_cur) - f(x_prev)) * (x_cur - x_prev)
        (x_prev, x_cur) = (x_cur, x_next)
        counter += 1
    print(f"x = {x_cur}. Iterations: {counter}")


def iteration(a, b):
    counter = 0
    x = phi(b)
    while abs(x - phi(x)) >= EPS:
        x = phi(x)
        counter += 1
    print(f"x = {x}. Iterations: {counter}")


if __name__ == "__main__":
    a = 0.5
    b = 0.6
    dichotomy(a, b, True)
    newton(a, b)
    modified_newton(a, b)
    chord(a, b)
    movable_chord(a, b)
    iteration(a, b)
