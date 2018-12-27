import numpy as np
import cv2
import argparse
import os.path as op
import os
from text_detect import detect_text


def four_point_transform(image, pts):
    def order_points(pts):
        rect = np.zeros((4, 2), dtype="float32")

        s = pts.sum(axis=1)
        rect[0] = pts[np.argmin(s)]
        rect[2] = pts[np.argmax(s)]

        diff = np.diff(pts, axis=1)
        rect[1] = pts[np.argmin(diff)]
        rect[3] = pts[np.argmax(diff)]

        return rect

    # obtain a consistent order of the points and unpack them
    # individually
    rect = order_points(pts)
    (tl, tr, br, bl) = rect

    # compute the width of the new image, which will be the
    # maximum distance between bottom-right and bottom-left
    # x-coordiates or the top-right and top-left x-coordinates
    widthA = np.sqrt(((br[0] - bl[0]) ** 2) + ((br[1] - bl[1]) ** 2))
    widthB = np.sqrt(((tr[0] - tl[0]) ** 2) + ((tr[1] - tl[1]) ** 2))
    maxWidth = max(int(widthA), int(widthB))

    # compute the height of the new image, which will be the
    # maximum distance between the top-right and bottom-right
    # y-coordinates or the top-left and bottom-left y-coordinates
    heightA = np.sqrt(((tr[0] - br[0]) ** 2) + ((tr[1] - br[1]) ** 2))
    heightB = np.sqrt(((tl[0] - bl[0]) ** 2) + ((tl[1] - bl[1]) ** 2))
    maxHeight = max(int(heightA), int(heightB))

    # now that we have the dimensions of the new image, construct
    # the set of destination points to obtain a "birds eye view",
    # (i.e. top-down view) of the image, again specifying points
    # in the top-left, top-right, bottom-right, and bottom-left
    # order
    dst = np.array([
        [0, 0],
        [maxWidth - 1, 0],
        [maxWidth - 1, maxHeight - 1],
        [0, maxHeight - 1]], dtype="float32")

    # compute the perspective transform matrix and then apply it
    M = cv2.getPerspectiveTransform(rect, dst)
    warped = cv2.warpPerspective(image, M, (maxWidth, maxHeight))

    # return the warped image
    return warped


def auto_scan_image(n):
    img = cv2.imread(n)

    print(n)
    # save_path = "new"
    # save_path = ("C:/Users/KLAP/Desktop/1/WindowsFormsApplication1/bin/Debug/namecard_img")
    save_path = (os.path.dirname(os.path.realpath(__file__)) + "\\namecard_img\\")
    print (save_path)
    extension = op.splitext(n)
    print("OOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO")
    tt = extension[0].split("\\")
    print("COUNT = " + str(len(tt)))
    print(tt[-1])
    print(extension[1])

    gray = cv2.cvtColor(img, cv2.COLOR_BGR2GRAY)
    blur = cv2.GaussianBlur(gray, (3, 3), 0)
    edged = cv2.Canny(blur, 75, 200)

    print("STEP 1 : Edge Detection")

    # cv2.namedWindow('Image', cv2.WINDOW_AUTOSIZE)
    # cv2.namedWindow('Edge', cv2.WINDOW_AUTOSIZE)
    # cv2.imshow("gray", gray)
    # cv2.imshow("Image", img)
    # cv2.imshow("Edged", edged)
    cv2.imwrite(op.join(save_path, tt[-1] + extension[1]), img)
    cv2.imwrite(op.join(save_path, tt[-1] + "_1" + extension[1]), gray)
    cv2.imwrite(op.join(save_path, tt[-1] + "_2" + extension[1]), blur)
    cv2.imwrite(op.join(save_path, tt[-1] + "_3" + extension[1]), edged)
    print ("@@@@@@@@@@@@@@@@@@@@@" + op.join(save_path, tt[-1] + "_gray" + extension[1]))

    # cv2.waitKey()
    # cv2.destroyAllWindows()

    _, cnts, _ = cv2.findContours(edged.copy(), cv2.RETR_LIST, cv2.CHAIN_APPROX_SIMPLE)
    cnts = sorted(cnts, key=cv2.contourArea, reverse=True)[:5]

    # loop over the contours
    for c in cnts:
        # approximate the contour
        peri = cv2.arcLength(c, True)
        approx = cv2.approxPolyDP(c, 0.02 * peri, True)

        # if our approximated contour has four points, then we
        # can assume that we have found our screen
        if len(approx) == 4:
            screenCnt = approx
            break

    # show the contour (outline) of the piece of paper

    cv2.drawContours(img, [screenCnt], -1, (0, 0, 255), 2)
    # cv2.imshow("Outline", img)
    # cv2.waitKey(0)
    cv2.imwrite(op.join(save_path, tt[-1] + "_4" + extension[1]), img)

    warped = four_point_transform(img, screenCnt.reshape(4, 2))
    #warped = cv2.cvtColor(warped, cv2.COLOR_BGR2GRAY)
    #warped = threshold_adaptive(warped, 251, offset=10).astype(np.uint8) * 255

    # cv2.imshow("Original", img)
    # cv2.imshow("Scanned", warped)
    cv2.imwrite(op.join(save_path, tt[-1] + "_5" + extension[1]), warped)

    print("OUT")
    print(tt[-1] + "_5" + extension[1])
    print()

    print("EEEEEEEEEEEEEEEEEEEEEEEEEEEE")
    # cv2.waitKey(0)

    # text_detect("/namecard_img/"+tt[-1] + "_5" + extension[1])
    # print(os.path.dirname(os.path.abspath(__file__)) + "/namecard_img/")
    detect_text(os.path.dirname(os.path.abspath(__file__)) + "/namecard_img/" + tt[-1] + "_5" + extension[1])
    # output_txt = open();


parser = argparse.ArgumentParser()
# argparse 모듈 에 ArgumentParse() 함수 사용하여 parser 생성

parser.add_argument("-img", "--img", required=True)
#  명령행 옵션을 지정하기 위해 사용합니다 명령행 옵션 인자는 -name으로 지정

args = parser.parse_args()
# parse에 add_argument()함수 사용해 args 인스턴스생성

people = args.img
# 명령행에서 받은 인자값을 people에 값을 넘겨줌

print (people)
auto_scan_image(people)
