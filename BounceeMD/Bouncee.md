<body style="
  background-color:#151d28; 
  color: #c7cfcc;  
  margin-top: 50px;
  margin-bottom: 50px;
  margin-right: 50px;
  margin-left: 50px;">

<style>
table th:first-of-type  { width: 4%;  }
table th:nth-of-type(2) { width: 35%; }
table th:nth-of-type(3) { width: 15%; }
</style>

# Bouncee maths

## Linear

| Name      | Function                              | Domain                                            | Graph                            |
| --------- | ------------------------------------- | ------------------------------------------------- | -------------------------------- |
| EaseIn    | $f(x) = x$                            | $\{ 0 \le x \le 1\}$                              | <img src="./easeLong/0ease.png"> |
| EaseSpike | $f(x) = 2 x$ <br />  $g(x) = 2 (1-x)$ | $\{ 0 \le x \le 0.5\}$ <br /> $\{0.5 < x \le 1\}$ | <img src="./easeLong/3ease.png"> |

## Sinus

| Name      | Function                                                | Domain                                            | Graph                            |
| --------- | ------------------------------------------------------- | ------------------------------------------------- | -------------------------------- |
| EaseIn    | $f(x) = -cos (0.5x \pi) + 1$                            | $\{ 0 \le x \le 1\}$                              | <img src="./easeLong/4ease.png"> |
| EaseOut   | $f(x) =  sin (0.5x \pi )$                               | $\{ 0 \le x \le 1\}$                              | <img src="./easeLong/5ease.png"> |
| EaseInOut | $f(x) = -0.5 cos(x \pi) + 0.5$                          | $\{ 0 \le x \le 1\}$                              | <img src="./easeLong/6ease.png"> |
| EaseSpike | $f(x) = -cos(x \pi) + 1$ <br /> $g(x) = cos(x \pi) + 1$ | $\{ 0 \le x \le 0.5\}$ <br /> $\{0.5 < x \le 1\}$ | <img src="./easeLong/7ease.png"> |

<div style="page-break-after: always;"></div> <br /> <br />

## Quadratic

| Name      | Function                                            | Domain                                            | Graph                             |
| --------- | --------------------------------------------------- | ------------------------------------------------- | --------------------------------- |
| EaseIn    | $f(x) = x^2$                                        | $\{ 0 \le x \le 1\}$                              | <img src="./easeLong/8ease.png">  |
| EaseOut   | $f(x) =  1 - (x - 1)^2$                             | $\{ 0 \le x \le 1\}$                              | <img src="./easeLong/9ease.png">  |
| EaseInOut | $f(x) = 2 x^2$ <br /> $g(x) = 1 - 0.5 (2  x - 2)^2$ | $\{ 0 \le x \le 0.5\}$ <br /> $\{0.5 < x \le 1\}$ | <img src="./easeLong/10ease.png"> |
| EaseSpike | $f(x) = 4 x^2$ <br /> $g(x) = (2  x - 2)^2$         | $\{ 0 \le x \le 0.5\}$ <br /> $\{0.5 < x \le 1\}$ | <img src="./easeLong/11ease.png"> |

## Cubic

| Name      | Function                                       | Domain                                            | Graph                             |
| --------- | ---------------------------------------------- | ------------------------------------------------- | --------------------------------- |
| EaseIn    | $f(x) = x^3$                                   | $\{ 0 \le x \le 1\}$                              | <img src="./easeLong/12ease.png"> |
| EaseOut   | $f(x) = 1 + (x - 1)^3$                         | $\{ 0 \le x \le 1\}$                              | <img src="./easeLong/13ease.png"> |
| EaseInOut | $f(x) = 4 x^3$ <br /> $g(x) = 1 + 4 (x - 1)^3$ | $\{ 0 \le x \le 0.5\}$ <br /> $\{0.5 < x \le 1\}$ | <img src="./easeLong/14ease.png"> |
| EaseSpike | $f(x) = 8 x^3$ <br /> $g(x) = -(2  x - 2)^3$   | $\{ 0 \le x \le 0.5\}$ <br /> $\{0.5 < x \le 1\}$ | <img src="./easeLong/15ease.png"> |

<div style="page-break-after: always;"></div> <br /> <br />

## Quartic

| Name      | Function                                                         | Domain                                            | Graph                             |
| --------- | ---------------------------------------------------------------- | ------------------------------------------------- | --------------------------------- |
| EaseIn    | $f(x) = x^4$                                                     | $\{ 0 \le x \le 1\}$                              | <img src="./easeLong/16ease.png"> |
| EaseOut   | $f(x) =  1 - (x - 1)^4$                                          | $\{ 0 \le x \le 1\}$                              | <img src="./easeLong/17ease.png"> |
| EaseInOut | $f(x) = 0.5 - 8 (x - 0.5)^4$ <br /> $g(x) = 0.5 + 8 (x - 0.5)^4$ | $\{ 0 \le x \le 0.5\}$ <br /> $\{0.5 < x \le 1\}$ | <img src="./easeLong/18ease.png"> |
| EaseSpike | $f(x) = 16 x^4$ <br /> $g(x) = (2  x - 2)^4$                     | $\{ 0 \le x \le 0.5\}$ <br /> $\{0.5 < x \le 1\}$ | <img src="./easeLong/19ease.png"> |

## Quintic

| Name      | Function                                         | Domain                                            | Graph                             |
| --------- | ------------------------------------------------ | ------------------------------------------------- | --------------------------------- |
| EaseIn    | $f(x) = x^5$                                     | $\{ 0 \le x \le 1\}$                              | <img src="./easeLong/20ease.png"> |
| EaseOut   | $f(x) = 1 + (x - 1)^5$                           | $\{ 0 \le x \le 1\}$                              | <img src="./easeLong/21ease.png"> |
| EaseInOut | $f(x) = 16 x^5$ <br /> $g(x) = 1 + 16 (x - 1)^5$ | $\{ 0 \le x \le 0.5\}$ <br /> $\{0.5 < x \le 1\}$ | <img src="./easeLong/22ease.png"> |
| EaseSpike | $f(x) = 32 x^5$ <br /> $g(x) = -(2  x - 2)^5$    | $\{ 0 \le x \le 0.5\}$ <br /> $\{0.5 < x \le 1\}$ | <img src="./easeLong/23ease.png"> |

<div style="page-break-after: always;"></div> <br /> <br />

## Exponential

| Name      | Function                                                                  | Domain                                            | Graph                             |
| --------- | ------------------------------------------------------------------------- | ------------------------------------------------- | --------------------------------- |
| EaseIn    | $f(x) = 1 - \sqrt{ 1 - x}$                                                | $\{ 0 \le x \le 1\}$                              | <img src="./easeLong/24ease.png"> |
| EaseOut   | $f(x) = \sqrt{x}$                                                         | $\{ 0 \le x \le 1\}$                              | <img src="./easeLong/25ease.png"> |
| EaseInOut | $f(x) = 0.5 - 0.5 \sqrt{1 - 2 x}$ <br /> $g(x) = 0.5 + 0.5 \sqrt{2x - 1}$ | $\{ 0 \le x \le 0.5\}$ <br /> $\{0.5 < x \le 1\}$ | <img src="./easeLong/26ease.png"> |
| EaseSpike | $f(x) = 1 - \sqrt{1 - 2 x}$ <br /> $g(x) = 1 + \sqrt{2x - 1}$             | $\{ 0 \le x \le 0.5\}$ <br /> $\{0.5 < x \le 1\}$ | <img src="./easeLong/27ease.png"> |

## Circular

| Name      | Function                                                                       | Domain                                            | Graph                             |
| --------- | ------------------------------------------------------------------------------ | ------------------------------------------------- | --------------------------------- |
| EaseIn    | $f(x) = 1 - \sqrt{ 1 - x^2}$                                                   | $\{ 0 \le x \le 1\}$                              | <img src="./easeLong/28ease.png"> |
| EaseOut   | $f(x) = \sqrt{ 1 - (x - 1)^2}$                                                 | $\{ 0 \le x \le 1\}$                              | <img src="./easeLong/29ease.png"> |
| EaseInOut | $f(x) = 0.5 - \sqrt{0.25 - x^2}$ <br /> $g(x) = 0.5 + \sqrt{0.25 - (x - 1)^2}$ | $\{ 0 \le x \le 0.5\}$ <br /> $\{0.5 < x \le 1\}$ | <img src="./easeLong/30ease.png"> |
| EaseSpike | $f(x) = 1 - \sqrt{1 - 4x^2}$ <br /> $g(x) = 1 - \sqrt{2x - 2}^2$               | $\{ 0 \le x \le 0.5\}$ <br /> $\{0.5 < x \le 1\}$ | <img src="./easeLong/31ease.png"> |

<div style="page-break-after: always;"></div> <br /> <br />

## Bounce

s = 7.5625 (scalar that narrows parabola) <br />
d = 2.75 (offset on the x axis)

| Name      | Function                                                                                                                                                                                     | Domain                                                                                                                                                                                                                                              | Graph                             |
| --------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | --------------------------------- |
| EaseIn    | $f(x) = 1 - sx^2$<br />$g(x) = 1 - s(x - \frac{1.5}{d})^2 - 0.75$<br />$h(x) = 1 - s(x - \frac{2.25}{d})^2$<br />$- 0.9375$ <br /> $i(x) = 1 - s(x - \frac{2.625}{d})^2$ <br /> $- 0.984375$ | $\{ 0 \le x < \frac{1}{d}\}$ <br />  $\{ 1/d \le x < \frac{2}{d}\}$ <br />  $\{ \frac{2}{d} \le x < \frac{2.5}{d}\}$ <br /><br />  $\{ \frac{2.5}{d} \le x < 1\}$ <br /><br />                                                                      | <img src="./easeLong/32ease.png"> |
| EaseOut   | $f(x) = sx^2$ <br /> $g(x) = s(x -\frac{1.5}{d})^2 - 0.75$ <br /> $h(x) = s(x - \frac{2.25})^2 - 0.9375$ <br /> $i(x) = s(x - \frac{2.625}{d})^2 - 0.984375$                                 | $\{ 0 \le x < \frac{1}{d}\}$ <br />  $\{ 1/d \le x < \frac{2}{d}\}$ <br />  $\{ \frac{2}{d} \le x < \frac{2.5}{d}\}$ <br />  $\{ \frac{2.5}{d} \le x < 1\}$                                                                                         | <img src="./easeLong/33ease.png"> |
| EaseInOut | $f(x) = sx^2$ <br /> $g(x) = s(x - 1.5/d)^2 - 0.75$ <br /> $h(x) = s(x - 2.25/d)^2 - 0.9375$ <br /> $i(x) = s(x - 2.625/d)^2 - 0.984375$                                                     | $\{ 0 \le x < 0.5d\}$ <br />  $\{ 0.5d \le x < 1/d\}$ <br />  $\{ 1/d \le x < d\}$ <br />  $\{ d \le x < 2/d\}$ <br />  $\{ 2/d \le x < 2.5/2d\}$ <br />  $\{ 2.5/2d \le x < 2.5/d\}$ <br />  $\{ 2.5/d \le x < 0.5\}$ <br />  $\{ 0.5 \le x < 1\}$ | <img src="./easeLong/34ease.png"> |
| EaseSpike | $f(x) = sx^2$ <br /> $g(x) = s(x - 1.5/d)^2 - 0.75$ <br /> $h(x) = s(x - 2.25/d)^2 - 0.9375$ <br /> $i(x) = s(x - 2.625/d)^2 - 0.984375$                                                     | $\{ 0 \le x < 0.5d\}$ <br />  $\{ 0.5d \le x < 1/d\}$ <br />  $\{ 1/d \le x < d\}$ <br />  $\{ d \le x < 2/d\}$ <br />  $\{ 2/d \le x < 2.5/2d\}$ <br />  $\{ 2.5/2d \le x < 2.5/d\}$ <br />  $\{ 2.5/d \le x < 0.5\}$ <br />  $\{ 0.5 \le x < 1\}$ | <img src="./easeLong/35ease.png"> |

## Elastic
## Back
  
Polynomial shaping:
## Inverted Cos
## Double Cubic
## Double Cubic Blend
## Double Odd

</body>