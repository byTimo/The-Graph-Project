﻿namespace UI.Infrastructure
{
    public interface IGraphObject
    {
        NodeStatus Status { get; }

        void ChangeViewToDefault();
        void ChangeView();
    }
}