using UnityEngine;
using UnityEngine.UI;
namespace HSVPicker
{
    [RequireComponent(typeof(RawImage)), ExecuteInEditMode()]
    public class ColorSliderImage : MonoBehaviour
    {

        /// <summary>
        /// Which value this slider can edit.
        /// </summary>

        public Slider.Direction direction;

        private RawImage image;
        [SerializeField] private bool onSaturation;
        [SerializeField] private bool onLights;
        [SerializeField] private double saturation;
        [SerializeField] private double lights;

        private RectTransform rectTransform
        {
            get
            {
                return transform as RectTransform;
            }
        }

        private void Awake()
        {
            image = GetComponent<RawImage>();

            if(Application.isPlaying)
                RegenerateTexture();
        }


        private void RegenerateTexture()
        {

            Texture2D texture;
            Color32[] colors;

            bool vertical = direction == Slider.Direction.BottomToTop || direction == Slider.Direction.TopToBottom;
            bool inverted = direction == Slider.Direction.TopToBottom || direction == Slider.Direction.RightToLeft;

            int size = 360;

            if (vertical)
                texture = new Texture2D(1, size);
            else
                texture = new Texture2D(size, 1);

            texture.hideFlags = HideFlags.DontSave;
            colors = new Color32[size];

                    for (int i = 0; i < size; i++)
                    {
                        double s = 1;
                        double v = 1;
                        if (onSaturation) s = saturation;
                        if (onLights) v = lights;
                        colors[inverted ? size - 1 - i : i] = ConvertHsvToRgb(i, s, v, 1);
                        
                    }


            texture.SetPixels32(colors);
            texture.Apply();

            if (image.texture != null)
                DestroyImmediate(image.texture);
            image.texture = texture;

            switch (direction)
            {
                case Slider.Direction.BottomToTop:
                case Slider.Direction.TopToBottom:
                    image.uvRect = new Rect(0, 0, 2, 1);
                    break;
                case Slider.Direction.LeftToRight:
                case Slider.Direction.RightToLeft:
                    image.uvRect = new Rect(0, 0, 1, 2);
                    break;
                default:
                    break;
            }
        }


        // Converts an HSV color to an RGB color.
        public static Color ConvertHsvToRgb(double h, double s, double v, float alpha)
        {

            double r = 0, g = 0, b = 0;

            if (s.Equals(0))
            {
                r = v;
                g = v;
                b = v;
            }

            else
            {
                int i;
                double f, p, q, t;


                if (h.Equals(360))
                    h = 0;
                else
                    h = h / 60;

                i = (int)(h);
                f = h - i;

                p = v * (1.0 - s);
                q = v * (1.0 - (s * f));
                t = v * (1.0 - (s * (1.0f - f)));


                switch (i)
                {
                    case 0:
                        r = v;
                        g = t;
                        b = p;
                        break;

                    case 1:
                        r = q;
                        g = v;
                        b = p;
                        break;

                    case 2:
                        r = p;
                        g = v;
                        b = t;
                        break;

                    case 3:
                        r = p;
                        g = q;
                        b = v;
                        break;

                    case 4:
                        r = t;
                        g = p;
                        b = v;
                        break;

                    default:
                        r = v;
                        g = p;
                        b = q;
                        break;
                }

            }

            return new Color((float)r, (float)g, (float)b, alpha);
        }
    }
}
