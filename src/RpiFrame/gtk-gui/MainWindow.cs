
// This file has been generated by the GUI designer. Do not modify.

public partial class MainWindow
{
	private global::Gtk.Fixed AlignmentBin;

	private global::Gtk.Image MainImage;

	protected virtual void Build()
	{
		global::Stetic.Gui.Initialize(this);
		// Widget MainWindow
		this.Name = "MainWindow";
		this.Title = global::Mono.Unix.Catalog.GetString("MainWindow");
		this.WindowPosition = ((global::Gtk.WindowPosition)(4));
		// Container child MainWindow.Gtk.Container+ContainerChild
		this.AlignmentBin = new global::Gtk.Fixed();
		this.AlignmentBin.Name = "AlignmentBin";
		this.AlignmentBin.HasWindow = false;
		// Container child AlignmentBin.Gtk.Fixed+FixedChild
		this.MainImage = new global::Gtk.Image();
		this.MainImage.WidthRequest = 500;
		this.MainImage.HeightRequest = 400;
		this.MainImage.Name = "MainImage";
		this.AlignmentBin.Add(this.MainImage);
		global::Gtk.Fixed.FixedChild w1 = ((global::Gtk.Fixed.FixedChild)(this.AlignmentBin[this.MainImage]));
		w1.X = 75;
		w1.Y = 61;
		this.Add(this.AlignmentBin);
		if ((this.Child != null))
		{
			this.Child.ShowAll();
		}
		this.DefaultWidth = 868;
		this.DefaultHeight = 571;
		this.Show();
		this.DeleteEvent += new global::Gtk.DeleteEventHandler(this.OnDeleteEvent);
	}
}
