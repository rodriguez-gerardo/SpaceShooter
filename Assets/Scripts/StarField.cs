﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


namespace UnityEngine
{

    public class StarField : MonoBehaviour
    {

        private Transform thisTransform;
        private ParticleSystem.Particle[] points;
        private float starDistanceSqr;
        private float starClipDistanceSqr;

        public Color starColor;
        public int starsMax = 600;
        public float starSize = 0.35f;
        public float starDistance = 60f;
        public float starClipDistance = 15f;



        // Use this for initialization
        void Start()
        {

            thisTransform = GetComponent<Transform>();
            starDistanceSqr = Convert.ToSingle((Math.Pow(starDistance, 2)));
            starClipDistanceSqr = Convert.ToSingle((Math.Pow(starClipDistance, 2)));


        }

        private void CreateStars()
        {
            points = new ParticleSystem.Particle[starsMax];

            for (int i = 0; i < starsMax; i++)
            {
                points[i].position = Random.insideUnitSphere * starDistance + thisTransform.position;
                points[i].startColor = new Color(starColor.r, starColor.g, starColor.b, starColor.a);
                points[i].startSize = starSize;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (points == null) CreateStars();

            for (int i = 0; i < starsMax; i++)
            {
                if ((points[i].position - thisTransform.position).sqrMagnitude > starDistanceSqr)
                {
                    points[i].position = Random.insideUnitSphere.normalized * starDistance + thisTransform.position;
                }

                if((points[i].position - thisTransform.position).sqrMagnitude <= starClipDistanceSqr)
                {
                    float percentage = (points[i].position - thisTransform.position).sqrMagnitude / starClipDistanceSqr;
                    points[i].startColor = new Color(1, 1, 1, percentage);
                    points[i].startSize = percentage * starSize;
                }
            }

            GetComponent<ParticleSystem>().SetParticles(points, points.Length);

        }
    }
}