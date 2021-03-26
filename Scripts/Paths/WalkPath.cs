using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;

public abstract class WalkPath : MonoBehaviour
{
    public enum EnumDir
    {
        Forward,
        Backward,
        HugLeft,
        HugRight,
        WeaveLeft,
        WeaveRight
    };

    protected float[] _distances;
    protected bool inverseDrawOrder = true; //if false is bugged, left for dbg purp

    [Tooltip("Objects of motion / Объекты движения")] public GameObject[] walkingPrefabs;
    [Tooltip("Number of paths / Количество путей")] public int numberOfWays = 1;
    [Tooltip("Space between paths / Пространство между путями")] public float lineSpacing = 1;
    [Tooltip("Density of movement of objects / Плотность движения объектов")] [Range(0.01f, 0.50f)] public float Density = 0.2f;
    [Tooltip("Distance between objects / Дистанция между объектами")] [SerializeField] [Range(1f, 10f)] protected float _minimalObjectLength = 1f;
    [Tooltip("Make the path closed in the ring / Сделать путь замкнутым в кольцо")] public bool loopPath;
    [Tooltip("Direction of movement / Направление движения. Левостороннее, правостороннее, итд.")] public EnumDir direction;

    [HideInInspector] public List<Vector3> pathPoint = new List<Vector3>();
    [HideInInspector] public List<GameObject> pathPointTransform = new List<GameObject>();
    [HideInInspector] public SpawnPoint[] SpawnPoints;
    [HideInInspector] public GameObject par;
    [HideInInspector] public PathType PathType = PathType.PeoplePath;
    [HideInInspector] public int[] pointLength = new int[2];
    [HideInInspector] public bool[] _forward;
    [HideInInspector] public bool disableLineDraw = false;
    public Vector3[,] points;

    /// <summary>
    /// Радиус сферы-стёрки [м]
    /// </summary>
	[Tooltip("Radius of the sphere-scraper [m] / Радиус сферы-стёрки [м]"), Range(0.1f, 25)]
    public float eraseRadius = 2f;

    /// <summary>
    /// Минимальное расстояние от курсора до линии при котором можно добавить новую точку в путь [м]
    /// </summary>
	[Tooltip("The minimum distance from the cursor to the line at which you can add a new point to the path [m] / Минимальное расстояние от курсора до линии, при котором можно добавить новую точку в путь [м]")] [Range(0.5f, 10)] public float addPointDistance = 2f;

    [Tooltip("Adjust the spawn of cars to the nearest surface. This option will be useful if there are bridges in the scene / Регулировка спавна автомобилей к ближайшей поверхности. Этот параметор будет полезен, если в сцене есть мосты.")] public float highToSpawn = 1.0f;

    #region Create And Delete Additional Points

    /// <summary>
    /// Идёт ли процесс создания новой точки
    /// </summary>
    [HideInInspector] public bool newPointCreation = false;

    /// <summary>
    /// Идёт ли процесс удаления некоторой старой точки
    /// </summary>
    [HideInInspector] public bool oldPointDeleting = false;

    /// <summary>
    /// Позиция мышки на экране 
    /// </summary>
    [HideInInspector] public Vector3 mousePosition = Vector3.zero;

    /// <summary>
    /// Индекс точки из массива которую хотят удалить
    /// </summary>
    private int deletePointIndex = -1;

    // точки между которыми будет создаваться дополнительная
    /// <summary>
    /// Индекс первой точки в массиве всех точек
    /// </summary>
    private int firstPointIndex = -1;

    /// <summary>
    /// Индекс второй точки в массиве всех точек
    /// </summary>
    private int secondPointIndex = -1;

    #endregion

    public Vector3 getNextPoint(int w, int index)
    {
        return points[w, index];
    }

    public Vector3 getStartPoint(int w)
    {
        return points[w, 1];
    }

    public int getPointsTotal(int w)
    {
        return pointLength[w];
    }

    private void Awake()
    {
        DrawCurved(false);

        if (!loopPath)
        {
            CreateSpawnPoints();
        }
    }

    public abstract void CreateSpawnPoints();
    public abstract void SpawnOnePeople(int w, bool forward);
    public abstract void SpawnPeople();

    public void DrawCurved(bool withDraw)
    {
        if (numberOfWays < 1) numberOfWays = 1;
        if (lineSpacing < 0.6f) lineSpacing = 0.6f;
        _forward = new bool[numberOfWays];

        for (int w = 0; w < numberOfWays; w++)
        {

            if (direction.ToString() == "Forward")
            {
                _forward[w] = true;
            }

            else if (direction.ToString() == "Backward")
            {
                _forward[w] = false;
            }

            else if (direction.ToString() == "HugLeft")
            {
                if ((w + 2) % 2 == 0)
                    _forward[w] = true;
                else
                    _forward[w] = false;
            }

            else if (direction.ToString() == "HugRight")
            {
                if ((w + 2) % 2 == 0)
                    _forward[w] = false;
                else
                    _forward[w] = true;
            }

            else if (direction.ToString() == "WeaveLeft")
            {
                if (w == 1 || w == 2 || (w - 1) % 4 == 0 || (w - 2) % 4 == 0)
                    _forward[w] = false;
                else _forward[w] = true;
            }

            else if (direction.ToString() == "WeaveRight")
            {
                if (w == 1 || w == 2 || (w - 1) % 4 == 0 || (w - 2) % 4 == 0)
                    _forward[w] = true;
                else _forward[w] = false;
            }

        }

        
        if (pathPoint.Count < 2) return;
        points = new Vector3[numberOfWays, pathPoint.Count + 2];

        pointLength[0] = pathPoint.Count + 2;

        for(int i = 0; i < pathPointTransform.Count; i++)
        {
            Vector3 vectorStart;
            Vector3 vectorEnd;
            if(i == 0)
            {
                if(loopPath)
                {
                    vectorStart = pathPointTransform[pathPointTransform.Count - 1].transform.position - pathPointTransform[i].transform.position;
                }
                else
                {
                    vectorStart = Vector3.zero;
                }
                vectorEnd = pathPointTransform[i].transform.position - pathPointTransform[i + 1].transform.position;
            }
            else if(i == pathPointTransform.Count - 1)
            {
                vectorStart = pathPointTransform[i - 1].transform.position - pathPointTransform[i].transform.position;
                if (loopPath)
                {
                    vectorEnd = pathPointTransform[i].transform.position - pathPointTransform[0].transform.position;
                }
                else
                {
                    vectorEnd = Vector3.zero;
                }
            }
            else
            {
                vectorStart = pathPointTransform[i - 1].transform.position - pathPointTransform[i].transform.position;
                vectorEnd = pathPointTransform[i].transform.position - pathPointTransform[i + 1].transform.position;
            }
            //
            Vector3 vectorShift = Vector3.Normalize((Quaternion.Euler(0, 90, 0) * (vectorStart + vectorEnd)));
            //
            points[0, i+1] = numberOfWays % 2 == 1 ? pathPointTransform[i].transform.position : pathPointTransform[i].transform.position + vectorShift * lineSpacing / 2;
            if(numberOfWays > 1)points[1, i+1] = points[0, i+1] - vectorShift * lineSpacing;
            //if (i == 0)
            //{

            //}
            //else
            //{
            for (int w = 1; w < numberOfWays; w++)
            {
                points[w, i+1] = points[0, i+1] + vectorShift * lineSpacing * (float)(Math.Pow(-1, w)) * ((w + 1) / 2);
            }
            //}
        }
        for (int w = 0; w < numberOfWays; w++)
        {
            points[w, 0] = points[w, 1];
            points[w, pointLength[0] - 1] = points[w, pointLength[0] - 2];
        }
        if (withDraw)
        {
            for (int w = 0; w < numberOfWays; w++)
            {
                if(loopPath)
                {
                    Gizmos.color = (_forward[w] ? Color.green : Color.red);
                    Gizmos.DrawLine(points[w, 0], points[w, pathPoint.Count]);
                }
                for (int i = 1; i < pathPoint.Count; i++)
                {
                    Gizmos.color = (_forward[w] ? Color.green : Color.red);
                    Gizmos.DrawLine(points[w, i+1], points[w, i]);
                }
            }
        }

        /*
        //расчет точек первой полосы
        for (int i = 0; i < pathPoint.Count; i++)
        {
            points[0, i + 1] = pathPointTransform[i].transform.position;
        }

        points[0, 0] = points[0, 1];
        points[0, pointLength[0] - 1] = points[0, pointLength[0] - 2];

        //отображение первой полосы
        for (int i = 0; i < pointLength[0]; i++)
            if (i != 0)
            {
                if (withDraw)
                {
                    Gizmos.color = (_forward[0] ? Color.green : Color.red);
                    Gizmos.DrawLine(points[0, i], points[0, i - 1]);
                }
            }
        if (loopPath)
            if (withDraw)
            {
                Gizmos.color = (_forward[0] ? Color.green : Color.red);
                Gizmos.DrawLine(points[0, 1], points[0, pointLength[0] - 2]);
            }

        //расчет остальных полос
        for (int w = 1; w < numberOfWays; w++)
        {

            if (numberOfWays > 1)//Тупо: сначала запуск цикла, потом проверка на необходимость запуска цикла
            {
                if (!loopPath)//пока не понял для чего
                {
                    Vector3 vectorStart = points[0, 2] - points[0, 1];
                    Vector3 pointVectorStart = vectorStart;
                    pointVectorStart = Quaternion.Euler(0, -90, 0) * pointVectorStart;

                    if (w % 2 == 0)
                        pointVectorStart = pointVectorStart.normalized * (float)(w * 0.5f * lineSpacing);
                    else if (w % 2 == 1)
                        pointVectorStart = pointVectorStart.normalized * (float)((w + 1) * 0.5f * lineSpacing);

                    Vector3 pointStart1 = Vector3.zero;
                    if (w % 2 == 1)
                        pointStart1 = (points[0, 1] - pointVectorStart);
                    else if (w % 2 == 0)
                        pointStart1 = (points[0, 1] + pointVectorStart);

                    pointStart1.y = points[0, 1].y;

                    points[w, 0] = pointStart1;
                    points[w, 1] = pointStart1;


                    Vector3 vectorFinish = points[0, pointLength[0] - 3] - points[0, pointLength[0] - 2];
                    Vector3 pointVectorFinish = vectorFinish;
                    pointVectorFinish = Quaternion.Euler(0, 90, 0) * pointVectorFinish;

                    if (w % 2 == 0)
                        pointVectorFinish = pointVectorFinish.normalized * (float)(w * 0.5f * lineSpacing);
                    else if (w % 2 == 1)
                        pointVectorFinish = pointVectorFinish.normalized * (float)((w + 1) * 0.5f * lineSpacing);

                    Vector3 pointFinish1 = Vector3.zero;

                    if (w % 2 == 1)
                        pointFinish1 = points[0, pointLength[0] - 2] - pointVectorFinish;
                    else if (w % 2 == 0)
                        pointFinish1 = points[0, pointLength[0] - 2] + pointVectorFinish;

                    pointFinish1.y = points[0, pointLength[0] - 2].y;

                    points[w, pointLength[0] - 2] = pointFinish1;
                    points[w, pointLength[0] - 1] = pointFinish1;
                }

                else
                {
                    Vector3 vectorNext = points[0, pointLength[0] - 2] - points[0, 1];
                    Vector3 vectorPrev = points[0, 1] - points[0, 2];

                    Vector3 pointVector1 = vectorPrev;
                    Vector3 pointVector2 = vectorNext;

                    float angle = Mathf.DeltaAngle(Mathf.Atan2(pointVector1.x, pointVector1.z) * Mathf.Rad2Deg,
                            Mathf.Atan2(pointVector2.x, pointVector2.z) * Mathf.Rad2Deg);

                    if (w % 2 == 0)
                        pointVector1 = pointVector1.normalized * (float)(w * 0.5f * lineSpacing);
                    else if (w % 2 == 1)
                        pointVector1 = pointVector1.normalized * (float)((w + 1) * 0.5f * lineSpacing);

                    pointVector1 = Quaternion.Euler(0, 90 + angle / 2, 0) * pointVector1;

                    Vector3 point1 = Vector3.zero;

                    if (w % 2 == 1)
                    {
                        point1 = points[0, 1] - pointVector1;
                    }
                    else if (w % 2 == 0)
                        point1 = points[0, 1] + pointVector1;


                    point1.y = points[0, 1].y;

                    points[w, 1] = point1;
                    points[w, 0] = point1;

                    Vector3 vectorNext1 = points[0, pointLength[0] - 2] - points[0, 1];
                    Vector3 vectorPrev1 = points[0, pointLength[0] - 3] - points[0, pointLength[0] - 2];

                    Vector3 pointVector11 = vectorPrev1;
                    Vector3 pointVector22 = vectorNext1;

                    float angle2 = Mathf.DeltaAngle(Mathf.Atan2(pointVector11.x, pointVector11.z) * Mathf.Rad2Deg,
                            Mathf.Atan2(pointVector22.x, pointVector22.z) * Mathf.Rad2Deg);

                    if (w % 2 == 0)
                        pointVector11 = pointVector11.normalized * (float)(w * 0.5f * lineSpacing);
                    else if (w % 2 == 1)
                        pointVector11 = pointVector11.normalized * (float)((w + 1) * 0.5f * lineSpacing);

                    pointVector11 = Quaternion.Euler(0, 90 + angle2 / 2, 0) * pointVector11;

                    Vector3 point11 = Vector3.zero;
                    if (w % 2 == 1)
                    {
                        point11 = points[0, pointLength[0] - 2] - pointVector11;
                    }
                    else if (w % 2 == 0)
                        point11 = points[0, pointLength[0] - 2] + pointVector11;


                    point11.y = points[0, pointLength[0] - 2].y;

                    points[w, pointLength[0] - 2] = point11;
                    points[w, pointLength[0] - 1] = point11;
                }

                for (int i = 2; i < pointLength[0] - 2; i++)//сам расчет точек
                {
                    Vector3 vectorNext = points[0, i] - points[0, i + 1];
                    Vector3 vectorPrev = points[0, i - 1] - points[0, i];

                    Vector3 pointVector1 = vectorPrev;
                    Vector3 pointVector2 = vectorNext;

                    float angle = Mathf.DeltaAngle(Mathf.Atan2(pointVector1.x, pointVector1.z) * Mathf.Rad2Deg,
                            Mathf.Atan2(pointVector2.x, pointVector2.z) * Mathf.Rad2Deg);

                    if (w % 2 == 0)
                        pointVector1 = pointVector1.normalized * (float)(w * 0.5f * lineSpacing);
                    else if (w % 2 == 1)
                        pointVector1 = pointVector1.normalized * (float)((w + 1) * 0.5f * lineSpacing);

                    pointVector1 = Quaternion.Euler(0, 90 + angle / 2, 0) * pointVector1;

                    Vector3 point1 = Vector3.zero;
                    if (w % 2 == 1)
                    {
                        point1 = points[0, i] - pointVector1;
                    }
                    else if (w % 2 == 0)
                        point1 = points[0, i] + pointVector1;


                    point1.y = points[0, i].y;

                    points[w, i] = point1;

                    if (withDraw)
                    {
                        Gizmos.color = (_forward[w] ? Color.green : Color.red);
                        Gizmos.DrawLine(points[w, i - 1], points[w, i]);
                    }
                }

                if (withDraw)
                {
                    Gizmos.color = (_forward[w] ? Color.green : Color.red);
                    Gizmos.DrawLine(points[w, pointLength[0] - 2], points[w, pointLength[0] - 3]);
                }

                if (withDraw && loopPath)
                {
                    Gizmos.color = (_forward[w] ? Color.green : Color.red);
                    Gizmos.DrawLine(points[w, 1], points[w, pointLength[0] - 2]);
                }
            }
        }
        //конец расчёта и отрисовки полос
        */
    }

#if UNITY_EDITOR
    public void OnDrawGizmos()
    {
        if (!disableLineDraw)
        {
            DrawCurved(true);
        }

        DrawNewPointCreation();
        DrawOldPointDeleting();
    }

    public void HideExistingIcons()
    {
        Transform t = transform.Find("points");

        foreach (Transform item in t)
        {
            DrawIcon(item.gameObject, 0, true);
        }
    }

    public void ShowExistingIcons()
    {
        Transform t = transform.Find("points");

        foreach (Transform item in t)
        {
            DrawIcon(item.gameObject, 1, false);
        }
    }

    private void DrawIcon(GameObject gameObject, int idx, bool basic)
    {
        GUIContent icon;

        if (!basic)
        {
            var largeIcons = GetTextures("sv_label_", string.Empty, 0, 8);
            icon = largeIcons[idx];
        }
        else
        {
            icon = EditorGUIUtility.IconContent("sv_icon_none");
        }

        var egu = typeof(EditorGUIUtility);
        var flags = System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic;
        var args = new object[] { gameObject, icon.image };
        var setIcon = egu.GetMethod("SetIconForObject", flags, null, new Type[] { typeof(UnityEngine.Object), typeof(Texture2D) }, null);

        if (basic)
        {
            setIcon.Invoke(null, new object[] { gameObject, null });
            return;
        }

        setIcon.Invoke(null, args);
    }

    private GUIContent[] GetTextures(string baseName, string postFix, int startIndex, int count)
    {
        GUIContent[] array = new GUIContent[count];

        for (int i = 0; i < count; i++)
        {
            array[i] = EditorGUIUtility.IconContent(baseName + (startIndex + i) + postFix);
        }

        return array;
    }


    /// <summary>
    /// Блокировка разблокировка эдитора
    /// </summary>
    /// <param name="lockValue">true - залочен, false - разлочен</param>
    public void EditorLock(bool lockValue)
    {
        ActiveEditorTracker.sharedTracker.isLocked = lockValue;
    }

    /// <summary>
    /// Рисует всё что связанно с добавлением новой точки в массив
    /// </summary>
    public void DrawNewPointCreation()
    {
        if (!newPointCreation)
        {
            return;
        }

        Selection.activeGameObject = gameObject;

        bool collizion = false;
        for (int i = 0; i < pathPoint.Count - 1; i++)
        {
            if (PointWithLineCollision(pathPointTransform[i].transform.position,
                pathPointTransform[i + 1].transform.position, mousePosition))
            {
                collizion = true;
                firstPointIndex = i;
                secondPointIndex = i + 1;
            }
        }

        if (collizion)
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.red;
            firstPointIndex = -1;
            secondPointIndex = -1;
        }

        Gizmos.DrawSphere(mousePosition, addPointDistance);
    }

    /// <summary>
    /// Рисует всё что связано с удалением старой точки из массива
    /// </summary>
    public void DrawOldPointDeleting()
    {
        if (!oldPointDeleting)
        {
            return;
        }

        Selection.activeGameObject = gameObject;

        bool collizion = false;
        for (int i = 0; i < pathPoint.Count; i++)
        {
            if (PointWithSphereCollision(mousePosition, pathPointTransform[i].transform.position))
            {
                collizion = true;
                deletePointIndex = i;
            }
        }

        if (collizion)
        {
            Gizmos.color = Color.magenta;
        }
        else
        {
            Gizmos.color = Color.cyan;
            deletePointIndex = -1;
        }

        Gizmos.DrawSphere(mousePosition, eraseRadius);
    }

#endif

    protected Vector3 GetRoutePosition(Vector3[] pointArray, float distance, int pointCount, bool loopPath)
    {
        int point = 0;
        float length = _distances[_distances.Length - 1];
        distance = Mathf.Repeat(distance, length);

        while (_distances[point] < distance)
        {
            ++point;
        }

        var point1N = ((point - 1) + pointCount) % pointCount;
        var point2N = point;

        var i = Mathf.InverseLerp(_distances[point1N], _distances[point2N], distance);
        return Vector3.Lerp(pointArray[point1N], pointArray[point2N], i);
    }

    protected int GetRoutePoint(float distance, int wayIndex, int pointCount, bool forward, bool loopPath)
    {
        int point = 0;
        float length = _distances[_distances.Length - 1];
        distance = Mathf.Repeat(distance, length);

        while (_distances[point] < distance)
        {
            ++point;
        }

        return point;
    }

    /// <summary>
    /// Проверка на столкновение сферы для стирания точек с точкой
    /// </summary>
    /// <param name="colisionSpherePosition">позиция сферы</param>
    /// <param name="pointPosition">позиция точки</param>
    private bool PointWithSphereCollision(Vector3 colisionSpherePosition, Vector3 pointPosition)
    {
        return Vector3.Magnitude(colisionSpherePosition - pointPosition) < eraseRadius;
    }

    /// <summary>
    /// Проверка на столкновение точки и линии
    /// </summary>
    /// <param name="pointPosition">Координаты новой точки которую планируется создать</param>
    /// <returns>True - есть столкновение, False - нет</returns>
    private bool PointWithLineCollision(Vector3 lineStartPosition, Vector3 lineEndPosition, Vector3 pointPosition)
    {
        return Distance(lineStartPosition, lineEndPosition, pointPosition) < addPointDistance;
    }

    /// <summary>
    /// Возвращает минимальное расстояние от точки до прямой [м]
    /// </summary>
    /// <param name="lineStartPosition">Координата начала прямой</param>
    /// <param name="lineEndPosition">Координата конца прямой</param>
    /// <param name="pointPosition">Координата точки</param>
    private float Distance(Vector3 lineStartPosition, Vector3 lineEndPosition, Vector3 pointPosition)
    {
        // квадрат длинны линии с началом в точке lineStartPosition и концом в точке lineEndPosition
        float l2 = Vector3.SqrMagnitude(lineEndPosition - lineStartPosition);

        if (l2 == 0f)
            return Vector3.Distance(pointPosition, lineStartPosition);
        float t = Mathf.Max(0,
            Mathf.Min(1, Vector3.Dot(pointPosition - lineStartPosition, lineEndPosition - lineStartPosition) / l2));
        Vector3 projection = lineStartPosition + t * (lineEndPosition - lineStartPosition);
        return Vector3.Distance(pointPosition, projection);
    }

    /// <summary>
    /// Добавляет новую точку между двумя созданными до этого
    /// </summary>
    public void AddPoint()
    {
        // Если индексы точек между которыми нужно создавать точку не выбраны
        // точка не создаётся
        if (firstPointIndex == -1 && secondPointIndex == firstPointIndex)
        {
            return;
        }

        var prefab = GameObject.Find("Population System").GetComponent<PopulationSystemManager>().pointPrefab;
        var obj = Instantiate(prefab, mousePosition, Quaternion.identity) as GameObject;
        obj.name = "p+";
        obj.transform.parent = pathPointTransform[firstPointIndex].transform.parent;
#if UNITY_EDITOR
        //if (dontDrawYoJunkFool)
        //    DrawIcon(obj, 0, true);
#endif
        pathPointTransform.Insert(firstPointIndex + 1, obj);
        pathPoint.Insert(firstPointIndex + 1, obj.transform.position);
    }

    /// <summary>
    /// Удаляет выбранную точку
    /// </summary>
    public void DeletePoint()
    {
        // Если индекс точек для удаления не выбран
        // точка не удаляется
        if (deletePointIndex == -1)
        {
            return;
        }

        DestroyImmediate(pathPointTransform[deletePointIndex]);

        pathPointTransform.RemoveAt(deletePointIndex);
        pathPoint.RemoveAt(deletePointIndex);
    }
}