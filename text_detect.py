import io
import os.path as op

# [START vision_text_detection]
def detect_text(path):
    from google.cloud import vision
    client = vision.ImageAnnotatorClient()

    # [START vision_python_migration_text_detection]
    with io.open(path, 'rb') as image_file:
        content = image_file.read()

    image = vision.types.Image(content=content)

    response = client.text_detection(image=image)
    texts = response.text_annotations
    print()
    print()
    print('Detect_Result =======')

    #print(texts)

    extension = op.splitext(path)
    print(extension)
    tt = extension[0].split("\\")


    a= list()
    for text in texts:
        #print('\n"{}"'.format(text.description))
        a.append('\n"{}"'.format(text.description))
        #print(text.description)
        #print("#####################################")

    print(a[0])
    print()
    output_txt = open(extension[0]+"_txt.txt",'wt',encoding='utf-8')
    output_txt.write(a[0])

    output_txt.close()