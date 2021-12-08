using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace PlaylistJesse.App_Code
{
    public class SongMethods
    {

        private DataSet ds = null;

        public SongMethods()
        {
        }
        public DataSet GetSongs()
        {
            string file = @"~\App_Data\Playlist.xml";
            ds = new DataSet("playlist");

            DataTable dtSongs = new DataTable("song");

            DataColumn dcId = new DataColumn("id");
            DataColumn dcTitle = new DataColumn("title");
            DataColumn dcArtist = new DataColumn("artist");
            DataColumn dcYear = new DataColumn("year");
            DataColumn dcGenre = new DataColumn("genre");
            DataColumn dcAlbum = new DataColumn("album");

            dtSongs.Columns.Add(dcId);
            dtSongs.Columns.Add(dcTitle);
            dtSongs.Columns.Add(dcArtist);
            dtSongs.Columns.Add(dcYear);
            dtSongs.Columns.Add(dcGenre);
            dtSongs.Columns.Add(dcAlbum);

            ds.Tables.Add(dtSongs);

            ds.ReadXml(HttpContext.Current.Server.MapPath(file));

            return ds;
        }

        public DataRow GetEmptyDataRow()
        {
            DataRow dr = ds.Tables["song"].NewRow();
            return dr;
        }

        public void CreateSong(DataRow dataRow)
        {
            string file = @"~\App_Data\Playlist.xml";
            ds.Tables["song"].Rows.Add(dataRow);
            ds.WriteXml(HttpContext.Current.Server.MapPath(file));
        }

        public void DeleteSong(string id)
        {
            string file = @"~\App_Data\Playlist.xml";
            DataRow[] drArray = ds.Tables["song"].Select("id = '" + id + "'");
            if (drArray != null && drArray.Length > 0)
            {
                drArray[0].Delete();
                ds.WriteXml(HttpContext.Current.Server.MapPath(file));
            }
        }

        public DataRow GetSong(string id)
        {
            GetSongs();
            foreach (DataRow dr in ds.Tables["song"].Rows)
            {
                if (dr["id"].ToString() == id.ToString())
                {
                    return dr;
                }
            }
            return null;
        }

        public void SaveSongs()
        {
            string file = @"~\App_Data\Playlist.xml";
            ds.WriteXml(HttpContext.Current.Server.MapPath(file));
        }
    }
}