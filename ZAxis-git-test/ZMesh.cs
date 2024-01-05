using System;
using System.Collections.Generic;

/* Delimitation Chars
: Declaration of Trigon
, New Vector Float
; New Vector
> Trigon End
< Trigon Start
| New Trigon
! EOF
~ Comment
*/

namespace ZAxis {
    public static class ZMesh {
        public static void ZMSHProcess(string zmsh) {
            var delimiters = new char[] { ':', ',', ';', '<', '>', '|', '!', '~' };
            var state = 0;
            var vState = 0;
            int pState = 0;

            var updZmshData = new List<char>();
            var updZmshStr = zmsh;

            var trigons = new List<Trigon>();


            // First Process

            updZmshStr = updZmshStr.Replace("\n", "");
            updZmshStr = updZmshStr.Replace(" ", "");

            // Second Process

            var updZmshStrSplit = updZmshStr.Split(delimiters);

            var curTrigon = "";
            var curTVXYZ = "";
            var curVec = new Vector3(-1, -1, -1);

            // 0:<0,0,0;0,1,0;1,0,0;>|!

            foreach (var c in updZmshStr) { 

                // Cmd.Out($"{state}");

                if (c == delimiters[0]) { // :
                    state = 1;
                }
                else if (c == delimiters[1]) { // ,
                    state = 3;
                }
                else if (c == delimiters[2]) { // ;

                }
                else if (c == delimiters[3]) { // <
                    state = 2;
                }
                
                // Cmd.Out($"{curTVXYZ}");
             
                if (state == 0) { // Before :
                    curTrigon = curTrigon + c;
                }
                else if (state == 1) { // After :

                    trigons.Add(new Trigon(new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0) ));
                    // Cmd.Out($"{trigons[Int32.Parse(curTrigon)]}");
                }
                else if (state == 2) { // '<'
                    curTVXYZ = curTVXYZ + c;
                    curTVXYZ = curTVXYZ.Replace("<", "");
                    curTVXYZ = curTVXYZ.Replace(",", "");
                    curTVXYZ = curTVXYZ.Replace(";", "");
                }
                else if (state == 3) { // ','
                    if (vState == 0) {
                        if (pState == 0) {
                            curVec.X = Int32.Parse(curTVXYZ);

                            curTVXYZ = "";

                            pState++;

                            state = 2;
                        }
                        else if (pState == 1) {
                            curVec.Y = Int32.Parse(curTVXYZ);

                            curTVXYZ = "";

                            pState++;

                            state = 2;
                        }
                        else if (pState == 2) {
                            curVec.Z = Int32.Parse(curTVXYZ);

                            curTVXYZ = "";

                            pState++;

                            state = 2;
                        }
                    }
                } 
            }

            // Cmd.Out($"{curVec.X} {curVec.Y} {curVec.Z}");
        }
    }
}