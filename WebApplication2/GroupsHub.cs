using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json;

namespace WebApplication2
{   
    [HubName("groupsHub")]
    public class GroupsHub:Hub
    {
        public static UserContext db = new UserContext();
        public void Hello()
        {
            Clients.All.hello();
        }
        /// <summary>
        /// 重写Hub连接事件
        /// </summary>
        /// <returns></returns>
        public override Task OnConnected()
        {
            var user = db.Users.SingleOrDefault(u => u.UserName == Context.ConnectionId);
            if (user == null)
            {
                user = new User()
                {
                   UserName=Context.ConnectionId
                };
                db.Users.Add(user);
            }
            var item = from a in db.Rooms
                       select new { a.RoomName };
            Clients.Client(this.Context.ConnectionId).getRoomlist(JsonConvert.SerializeObject(item.ToList()));
            return base.OnConnected();
        }
        /// <summary>
        /// 更新所有用户的房间列表
        /// </summary>
        public void GetRoomList()
        {
            var item = from a in db.Rooms
                       select new { a.RoomName };
            string jsondata = JsonConvert.SerializeObject(item.ToList());
        }
        /// <summary>
        /// 重写Hub连接断开事件
        /// </summary>
        /// <param name="stopCalled"></param>
        /// <returns></returns>
        public override Task OnDisconnected(bool stopCalled)
        {
            var user = db.Users.Where(t => t.UserName == Context.ConnectionId).FirstOrDefault();
            if (user != null)
            {
                db.Users.Remove(user);
                foreach (var item in user.Rooms)
                {

                }
            }
            return base.OnDisconnected(stopCalled);
        }
        /// <summary>
        /// 加入房间
        /// </summary>
        /// <param name="roomName"></param>
        public void AddToRoom(string roomName)
        {
            var room = db.Rooms.Find(t => t.RoomName == roomName);
            if (room != null)
            {
                var isuser = room.Users.Where(t => t.UserName == Context.ConnectionId).FirstOrDefault();
                if (isuser == null)
                {
                    var user = db.Users.Find(t => t.UserName == Context.ConnectionId);
                    user.Rooms.Add(room);
                    room.Users.Add(user);
                    Groups.Add(Context.ConnectionId, roomName);
                    Clients.Client(Context.ConnectionId).addRoom(roomName);
                }
                else
                {
                    Clients.Client(Context.ConnectionId).showMessage("请勿重复加入房间!");

                }
            }
        }
        /// <summary>
        /// 创建聊天室
        /// </summary>
        /// <param name="roomName"></param>
        public void CreatRoom(string roomName)
        {
            var room = db.Rooms.Find(t => t.RoomName == roomName);
            if (room == null)
            {
                ConversationRoom cr = new ConversationRoom
                {
                    RoomName = roomName
                };
                db.Rooms.Add(cr);
                AddToRoom(roomName);
                Clients.Client(Context.ConnectionId).showMessage("房间创建完成!");
                GetRoomList();
            }
            else
            {
                Clients.Client(Context.ConnectionId).showMessage("房间名重复!");
            }
        }
        /// <summary>
        /// 退出聊天室
        /// </summary>
        /// <param name="roomName"></param>
        public void RemoveFromRoom(string roomName)
        {
            var room = db.Rooms.Find(t => t.RoomName == roomName);
            if (room != null)
            {
                var user = room.Users.Where(t => t.UserName == Context.ConnectionId).FirstOrDefault();
                room.Users.Remove(user);
                if (room.Users.Count <= 0)
                {
                    db.Rooms.Remove(room);
                }
                Groups.Remove(Context.ConnectionId, roomName);
                Clients.Client(Context.ConnectionId).removeRoom("退出成功!");
            }
        }
        /// <summary>
        /// 给分组内所有的用户发送消息
        /// </summary>
        /// <param name="Room"></param>
        /// <param name="Message"></param>
        public void SendMessage(string Room, string Message)
        {
            Clients.Group(Room, new string[0]).sendMessage(Room, Message + " " + DateTime.Now.ToString());
        }
    }
}