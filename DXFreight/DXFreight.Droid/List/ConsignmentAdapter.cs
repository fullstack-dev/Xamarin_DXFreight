using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;

namespace DXFreight.Droid.List
{
    class ConsignmentAdapter : BaseAdapter<Consignment>
    {
        private Context mContext;
        private int mRowLayout;
        private List<Consignment> mConsignments;
        private int[] mAlternatingColors;

        public ConsignmentAdapter(Context context, int rowLayout, List<Consignment> consignment)
        {
            mContext = context;
            mRowLayout = rowLayout;
            mConsignments = consignment;
            mAlternatingColors = new int[] { 0xF2F2F2, 0x009900 };
        }

        public override int Count
        {
            get { return mConsignments.Count; }
        }

        public override Consignment this[int position]
        {
            get { return mConsignments[position]; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;

            if (row == null)
            {
                row = LayoutInflater.From(mContext).Inflate(mRowLayout, parent, false);
            }

            //row.SetBackgroundColor(GetColorFromInteger(mAlternatingColors[position % mAlternatingColors.Length]));
            CheckBox txtCheckBox = row.FindViewById<CheckBox>(Resource.Id.txtCheckBox);
            txtCheckBox.Checked = mConsignments[position].CheckBox;

            TextView cT = row.FindViewById<TextView>(Resource.Id.txtCT);
            cT.Text = mConsignments[position].CT;

            TextView conNum = row.FindViewById<TextView>(Resource.Id.txtConNum);
            conNum.Text = mConsignments[position].ConNum;

            TextView items = row.FindViewById<TextView>(Resource.Id.txtItems);
            items.Text = mConsignments[position].Items;

            TextView times = row.FindViewById<TextView>(Resource.Id.txtTimes);
            times.Text = mConsignments[position].Times;

            TextView tS = row.FindViewById<TextView>(Resource.Id.txtTS);
            tS.Text = mConsignments[position].TS;

            TextView address = row.FindViewById<TextView>(Resource.Id.txtAddress);
            address.Text = mConsignments[position].Address;

            //if ((position % 2) == 1)
            //{
                //Green background, set text white
            //    cT.SetTextColor(Color.White);
            //    conNum.SetTextColor(Color.White);
            //    items.SetTextColor(Color.White);
            //    times.SetTextColor(Color.White);
            //    tS.SetTextColor(Color.White);
            //    address.SetTextColor(Color.White);
            //}

            //else
            //{
                //White background, set text black
            //    cT.SetTextColor(Color.Black);
            //    conNum.SetTextColor(Color.Black);
            //    items.SetTextColor(Color.Black);
            //    times.SetTextColor(Color.Black);
            //    tS.SetTextColor(Color.Black);
            //    address.SetTextColor(Color.Black);
            //}

            return row;
        }

        private Color GetColorFromInteger(int color)
        {
            return Color.Rgb(Color.GetRedComponent(color), Color.GetGreenComponent(color), Color.GetBlueComponent(color));
        }
    }
}